using Microsoft.Win32; // Windows kayıt defteri işlemleri için kullanılan kütüphane
using UserFetchSteam.API.Types; // UserFetchSteam API tipleri için gerekli using ifadesi
using System; // Genel C# sistem kütüphanesi
using System.IO; // Dosya işlemleri için kullanılan kütüphane
using System.Runtime.InteropServices; // Platforma özgü işlev çağrıları için kullanılan kütüphane

namespace UserFetchSteam
{
    // Steam istemci işlemlerini yöneten Steam sınıfı
    public static class Steam
    {
        private static IntPtr Handle = IntPtr.Zero; // Steam kitaplığının işaretçisi
        private static Steam.NativeCreateInterface CallCreateInterface; // CreateInterface işlevini çağıran temsilci
        private static Steam.NativeSteamBGetCallback CallSteamBGetCallback; // Steam_BGetCallback işlevini çağıran temsilci
        private static Steam.NativeSteamFreeLastCallback CallSteamFreeLastCallback; // Steam_FreeLastCallback işlevini çağıran temsilci

        // İşlev işaretçisini alma işlevini kullanarak bir temsilciyi alma metodu
        private static Delegate GetExportDelegate<TDelegate>(IntPtr module, string name)
        {
            IntPtr procAddress = Steam.Native.GetProcAddress(module, name);
            if (procAddress == IntPtr.Zero)
                return (Delegate)null;

            return Marshal.GetDelegateForFunctionPointer(procAddress, typeof(TDelegate));
        }

        // İşlev işaretçisini alma işlevini kullanarak bir işlevi alma metodu
        private static TDelegate GetExportFunction<TDelegate>(IntPtr module, string name) where TDelegate : class
        {
            return (TDelegate)Convert.ChangeType(Steam.GetExportDelegate<TDelegate>(module, name), typeof(TDelegate));
        }

        // Steam kurulum yolunu almayı sağlayan metot
        public static string GetInstallPath()
        {
            return (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\Software\\Valve\\Steam", "InstallPath", (object)null);
        }

        // Belirli bir sürüm ve arabirim türünde bir nesne arabirimi oluşturan metot
        public static TClass CreateInterface<TClass>(string version) where TClass : INativeWrapper, new()
        {
            IntPtr objectAddress = Steam.CallCreateInterface(version, IntPtr.Zero);
            if (objectAddress == IntPtr.Zero)
                return default(TClass);

            TClass @class = new TClass();
            @class.SetupFunctions(objectAddress);
            return @class;
        }

        // Geri çağırım mesajını alma metodu
        public static bool GetCallback(int pipe, ref CallbackMessage message, ref int call)
        {
            return Steam.CallSteamBGetCallback(pipe, ref message, ref call);
        }

        // Son geri çağırımı serbest bırakma metodu
        public static bool FreeLastCallback(int pipe)
        {
            return Steam.CallSteamFreeLastCallback(pipe);
        }

        // Steam kitaplığını yükleme metodu
        public static bool Load()
        {
            if (Steam.Handle != IntPtr.Zero)
                return true;

            string installPath = Steam.GetInstallPath();
            if (installPath == null)
                return false;

            // Dll yollarını ayarla
            Steam.Native.SetDllDirectory(installPath + ";" + Path.Combine(installPath, "bin"));

            // steamclient.dll'i yükle
            IntPtr module = Steam.Native.LoadLibraryEx(Path.Combine(installPath, "steamclient.dll"), IntPtr.Zero, 8U);
            if (module == IntPtr.Zero)
                return false;

            // İşlevleri al ve temsilcileri ata
            Steam.CallCreateInterface = Steam.GetExportFunction<Steam.NativeCreateInterface>(module, "CreateInterface");
            if (Steam.CallCreateInterface == null)
                return false;

            Steam.CallSteamBGetCallback = Steam.GetExportFunction<Steam.NativeSteamBGetCallback>(module, "Steam_BGetCallback");
            if (Steam.CallSteamBGetCallback == null)
                return false;

            Steam.CallSteamFreeLastCallback = Steam.GetExportFunction<Steam.NativeSteamFreeLastCallback>(module, "Steam_FreeLastCallback");
            if (Steam.CallSteamFreeLastCallback == null)
                return false;

            Steam.Handle = module; // Kitaplık işaretçisini ata
            return true; // Yükleme başarılı oldu
        }

        // Platforma özgü işlevleri içeren Native yapısı
        [StructLayout(LayoutKind.Sequential, Size = 1)]
        private struct Native
        {
            internal const uint LOAD_WITH_ALTERED_SEARCH_PATH = 8;

            // Dll işlevlerini bildiren platforma özgü işlev tanımlamaları
            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern IntPtr LoadLibraryEx(string lpszLib, IntPtr hFile, uint dwFlags);

            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern IntPtr SetDllDirectory(string lpPathName);
        }

        // CreateInterface işlevini çağıran temsilciyi tanımlayan yapısı
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate IntPtr NativeCreateInterface(string version, IntPtr returnCode);

        // Steam_BGetCallback işlevini çağıran temsilciyi tanımlayan yapısı
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private delegate bool NativeSteamBGetCallback(int pipe, ref CallbackMessage message, ref int call);

        // Steam_FreeLastCallback işlevini çağıran temsilciyi tanımlayan yapısı
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private delegate bool NativeSteamFreeLastCallback(int pipe);
    }
}
