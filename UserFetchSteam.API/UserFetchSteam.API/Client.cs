using UserFetchSteam.API.Types; // UserFetchSteam API tipleri için gerekli using ifadesi
using UserFetchSteam.API.Wrappers; // UserFetchSteam API sınıf sarmalları için gerekli using ifadesi
using System; // Genel C# sistem kütüphanesi
using System.Collections.Generic; // Koleksiyon tiplerini kullanmak için gerekli kütüphane
using System.Linq; // LINQ sorguları için gerekli kütüphane

namespace UserFetchSteam
{
    // Steam istemci işlemlerini yöneten Client sınıfı
    public class Client
    {
        private List<ICallback> Callbacks = new List<ICallback>(); // Geri çağırım nesnelerinin listesi
        public SteamClient009 SteamClient; // Steam istemci nesnesi
        public SteamUser012 SteamUser; // Steam kullanıcı nesnesi
        public SteamUserStats007 SteamUserStats; // Steam kullanıcı istatistikleri nesnesi
        private int Pipe; // Steam bağlantı kanalı
        private int User; // Steam kullanıcı kimliği
        private bool RunningCallbacks; // Geriçağırımların çalışıp çalışmadığını tutan bayrak

        // Sınıfın yıkıcı (finalizer) metodu
        ~Client()
        {
            if (this.SteamClient == null)
                return;

            // Kullanıcıyı serbest bırak ve bağlantıyı kapat
            this.SteamClient.ReleaseUser(this.Pipe, this.User);
            this.User = 0;
            this.SteamClient.ReleaseSteamPipe(this.Pipe);
            this.Pipe = 0;
        }

        // Steam istemciyi başlatan metot
        public bool Initialize(long appId)
        {
            if (appId != 0L)
                Environment.SetEnvironmentVariable("SteamAppId", appId.ToString());

            // Steam kurulum yolu alınıyor ve yükleniyor
            if (Steam.GetInstallPath() == null || !Steam.Load())
                return false;

            // Steam istemci arabirimini oluştur
            this.SteamClient = Steam.CreateInterface<SteamClient009>("SteamClient009");
            if (this.SteamClient == null)
                return false;

            // Steam bağlantı kanalı oluştur
            this.Pipe = this.SteamClient.CreateSteamPipe();
            if (this.Pipe == 0)
                return false;

            // Küresel Steam kullanıcısına bağlan
            this.User = this.SteamClient.ConnectToGlobalUser(this.Pipe);
            if (this.User == 0)
                return false;

            // Steam kullanıcı ve istatistik nesnelerini al
            this.SteamUser = this.SteamClient.GetSteamUser012(this.User, this.Pipe);
            this.SteamUserStats = this.SteamClient.GetSteamUserStats006(this.User, this.Pipe);

            return true;
        }

        // Belirli türde bir geri çağırım nesnesi oluşturan ve kaydeden metot
        public TCallback CreateAndRegisterCallback<TCallback>() where TCallback : ICallback, new()
        {
            // Geri çağırım nesnesi oluştur ve kaydet
            TCallback callback = new TCallback();
            this.Callbacks.Add((ICallback)callback);
            return callback;
        }

        // Geri çağırımları işleyen metot
        public void RunCallbacks(bool server)
        {
            if (this.RunningCallbacks)
                return;

            this.RunningCallbacks = true;
            CallbackMessage message = new CallbackMessage();
            int call = 0;

            // Geri çağırım mesajlarını işle
            while (Steam.GetCallback(this.Pipe, ref message, ref call))
            {
                // Belirli tipteki geri çağırımları çalıştır
                foreach (ICallback callback in this.Callbacks.Where<ICallback>((Func<ICallback, bool>)(candidate =>
                {
                    if (candidate.Id == message.m_iCallback)
                        return candidate.Server == server;

                    return false;

                }))) callback.Run(message.m_pubParam);

                // Son geri çağırımı serbest bırak
                Steam.FreeLastCallback(this.Pipe);
            }

            this.RunningCallbacks = false;
        }
    }
}
