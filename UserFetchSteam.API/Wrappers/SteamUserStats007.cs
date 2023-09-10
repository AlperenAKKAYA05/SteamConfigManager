// Decompiled with JetBrains decompiler
// Bu kod JetBrains firmasının bir decompiler aracıyla çözümlenmiş gibi görünüyor.
// Bu, derlenmiş bir DLL dosyasının içeriğini orijinal kaynak kod hâline dönüştürmeye yardımcı olur.

// İlgili using ifadeleri ve gerekli kütüphanelerin eklenmesi
using UserFetchSteam.API.Interfaces; // UserFetchSteam API arayüzleri için gerekli using ifadesi
using System; // Genel C# sistem kütüphanesi
using System.Runtime.InteropServices; // Platforma özgü işlev çağrıları için kullanılan kütüphane

namespace UserFetchSteam.API.Wrappers
{
    // Steam API'daki ISteamUserStats007 arabirimini saran bir sınıf tanımı
    public class SteamUserStats007 : NativeWrapper<ISteamUserStats007>
    {
        // Kullanıcının mevcut istatistiklerini isteme işlemini başlatan metot
        [return: MarshalAs(UnmanagedType.I1)]
        public bool RequestCurrentStats()
        {
            return this.Call<bool, SteamUserStats007.NativeRequestCurrentStats>(this.Functions.RequestCurrentStats, new object[1]
            {
                (object) this.ObjectAddress
            });
        }

        // Belirli bir istatistik değerini almayı sağlayan metotlar
        [return: MarshalAs(UnmanagedType.I1)]
        public bool GetStatValue(string name, ref int value)
        {
            return this.GetFunction<SteamUserStats007.NativeGetStatInt>(this.Functions.GetStatInteger)(this.ObjectAddress, name, ref value);
        }

        [return: MarshalAs(UnmanagedType.I1)]
        public bool GetStatValue(string name, ref float value)
        {
            return this.GetFunction<SteamUserStats007.NativeGetStatFloat>(this.Functions.GetStatFloat)(this.ObjectAddress, name, ref value);
        }

        // Belirli bir istatistik değerini ayarlamayı sağlayan metotlar
        [return: MarshalAs(UnmanagedType.I1)]
        public bool SetStatValue(string name, int value)
        {
            return this.Call<bool, SteamUserStats007.NativeSetStatInt>(this.Functions.SetStatInteger, (object)this.ObjectAddress, (object)name, (object)value);
        }

        [return: MarshalAs(UnmanagedType.I1)]
        public bool SetStatValue(string name, float value)
        {
            return this.Call<bool, SteamUserStats007.NativeSetStatFloat>(this.Functions.SetStatFloat, (object)this.ObjectAddress, (object)name, (object)value);
        }

        // Bir başarı durumunu sorgulama ve ayarlama metotları
        [return: MarshalAs(UnmanagedType.I1)]
        public bool GetAchievementState(string name, ref bool achieved)
        {
            return this.GetFunction<SteamUserStats007.NativeGetAchievement>(this.Functions.GetAchievement)(this.ObjectAddress, name, ref achieved);
        }

        [return: MarshalAs(UnmanagedType.I1)]
        public bool SetAchievement(string name, bool state)
        {
            if (!state)
                return this.Call<bool, SteamUserStats007.NativeClearAchievement>(this.Functions.ClearAchievement, (object)this.ObjectAddress, (object)name);
            return this.Call<bool, SteamUserStats007.NativeSetAchievement>(this.Functions.SetAchievement, (object)this.ObjectAddress, (object)name);
        }

        // İstatistikleri sunucuya kaydetmeyi sağlayan metot
        [return: MarshalAs(UnmanagedType.I1)]
        public bool StoreStats()
        {
            return this.Call<bool, SteamUserStats007.NativeStoreStats>(this.Functions.StoreStats, new object[1]
            {
                (object) this.ObjectAddress
            });
        }

        // Bir başarı ikonunun kimliğini almayı sağlayan metot
        public int GetAchievementIcon(string name)
        {
            return this.Call<int, SteamUserStats007.NativeGetAchievementIcon>(this.Functions.GetAchievementIcon, (object)this.ObjectAddress, (object)name);
        }

        // Bir başarı için görüntüleme özniteliğini almayı sağlayan metot
        public string GetAchievementDisplayAttribute(string name, string key)
        {
            return this.Call<string, SteamUserStats007.NativeGetAchievementDisplayAttribute>(this.Functions.GetAchievementDisplayAttribute, (object)this.ObjectAddress, (object)name, (object)key);
        }

        // Tüm istatistikleri sıfırlamayı sağlayan metot
        [return: MarshalAs(UnmanagedType.I1)]
        public bool ResetAllStats(bool achievementsToo)
        {
            return this.Call<bool, SteamUserStats007.NativeResetAllStats>(this.Functions.ResetAllStats, (object)this.ObjectAddress, (object)achievementsToo);
        }

        // Aşağıdaki delege tanımları, platforma özgü işlev çağrıları için kullanılır
        // İşlevlere doğrudan erişimi sağlamak amacıyla bu delege tanımları kullanılır

        // Mevcut istatistikleri isteme işlevinin delege tanımı
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool NativeRequestCurrentStats(IntPtr thisObject);

        // Belirli bir tamsayı istatistik değerini almak için kullanılan delege tanımı
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool NativeGetStatInt(IntPtr thisObject, string pchName, ref int pData);

        // Belirli bir ondalık sayı istatistik değerini almak için kullanılan delege tanımı
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool NativeGetStatFloat(IntPtr thisObject, string pchName, ref float pData);

        // Belirli bir tamsayı istatistik değerini ayarlamak için kullanılan delege tanımı
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool NativeSetStatInt(IntPtr thisObject, string pchName, int nData);

        // Belirli bir ondalık sayı istatistik değerini ayarlamak için kullanılan delege tanımı
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool NativeSetStatFloat(IntPtr thisObject, string pchName, float fData);

        // Bir başarı durumunu sorgulamak için kullanılan delege tanımı
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool NativeGetAchievement(IntPtr thisObject, string pchName, ref bool pbAchieved);

        // Bir başarıyı ayarlamak için kullanılan delege tanımı
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool NativeSetAchievement(IntPtr thisObject, string pchName);

        // Bir başarının temizlenmesi için kullanılan delege tanımı
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        private delegate bool NativeClearAchievement(IntPtr thisObject, string pchName);

        // İstatistikleri sunucuya kaydetmek için kullanılan delege tanımı
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool NativeStoreStats(IntPtr thisObject);

        // Bir başarı ikonunun kimliğini almak için kullanılan delege tanımı
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int NativeGetAchievementIcon(IntPtr thisObject, string pchName);

        // Bir başarı için görüntüleme özniteliğini almak için kullanılan delege tanımı
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate string NativeGetAchievementDisplayAttribute(IntPtr thisObject, string pchName, string pchKey);

        // Tüm istatistikleri sıfırlamak için kullanılan delege tanımı
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool NativeResetAllStats(IntPtr thisObject, bool bAchievementsToo);
    }
}
