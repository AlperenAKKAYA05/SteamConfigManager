// Decompiled with JetBrains decompiler
// Bu kod JetBrains firmasının bir decompiler aracıyla çözümlenmiş gibi görünüyor.
// Bu, derlenmiş bir DLL dosyasının içeriğini orijinal kaynak kod hâline dönüştürmeye yardımcı olur.

// İlgili using ifadeleri ve gerekli kütüphanelerin eklenmesi
using UserFetchSteam.API.Interfaces; // UserFetchSteam API arayüzleri için gerekli using ifadesi
using System; // Genel C# sistem kütüphanesi
using System.Runtime.InteropServices; // Platforma özgü işlev çağrıları için kullanılan kütüphane

namespace UserFetchSteam.API.Wrappers
{
    // Steam API'daki ISteamUser012 arabirimini saran bir sınıf tanımı
    public class SteamUser012 : NativeWrapper<ISteamUser012>
    {
        // Kullanıcının Steam'e giriş yapılıp yapmadığını kontrol eden metot
        [return: MarshalAs(UnmanagedType.I1)]
        public bool IsLoggedIn()
        {
            return this.Call<bool, SteamUser012.NativeLoggedOn>(this.Functions.LoggedOn, new object[1]
            {
                (object) this.ObjectAddress
            });
        }

        // Kullanıcının Steam Kimliğini (ID) almayı sağlayan metot
        public ulong GetSteamID()
        {
            SteamUser012.NativeGetSteamID function = this.GetFunction<SteamUser012.NativeGetSteamID>(this.Functions.GetSteamID);
            ulong steamId = 0;
            function(this.ObjectAddress, ref steamId);
            return steamId;
        }

        // Aşağıdaki iki delege, platforma özgü işlev çağrıları için kullanılır
        // İşlevlere doğrudan erişimi sağlamak amacıyla bu delege tanımları kullanılır

        // Steam kullanıcısının giriş yapılıp yapmadığını kontrol eden işlevin delege tanımı
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool NativeLoggedOn(IntPtr thisObject);

        // Steam Kimliği (ID) almak için kullanılan işlevin delege tanımı
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeGetSteamID(IntPtr thisObject, ref ulong steamId);
    }
}
