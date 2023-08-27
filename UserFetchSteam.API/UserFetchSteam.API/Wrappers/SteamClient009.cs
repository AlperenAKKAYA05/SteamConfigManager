// Decompiled with JetBrains decompiler
// Bu kod JetBrains firmasının bir decompiler aracıyla çözümlenmiş gibi görünüyor.
// Bu, derlenmiş bir DLL dosyasının içeriğini orijinal kaynak kod hâline dönüştürmeye yardımcı olur.

using UserFetchSteam.API.Interfaces;
using UserFetchSteam.API.Types;
using System;
using System.Runtime.InteropServices;

namespace UserFetchSteam.API.Wrappers
{
    // SteamClient009 sınıfını sarmalayan sınıf
    public class SteamClient009 : NativeWrapper<ISteamClient009>
    {
        // Steam boru hattı oluşturur
        public int CreateSteamPipe()
        {
            return this.Call<int, SteamClient009.NativeCreateSteamPipe>(this.Functions.CreateSteamPipe, new object[1]
            {
                (object) this.ObjectAddress
            });
        }

        // Steam boru hattını serbest bırakır
        public bool ReleaseSteamPipe(int pipe)
        {
            return this.Call<bool, SteamClient009.NativeReleaseSteamPipe>(this.Functions.ReleaseSteamPipe, new object[2]
            {
                (object) this.ObjectAddress, pipe
            });
        }

        // Yerel kullanıcı oluşturur ve bir boru hattına bağlar
        public int CreateLocalUser(ref int pipe, AccountType type)
        {
            return this.GetFunction<SteamClient009.NativeCreateLocalUser>(this.Functions.CreateLocalUser)(this.ObjectAddress, ref pipe, type);
        }

        // Küresel Steam kullanıcısına bir boru hattıyla bağlanır
        public int ConnectToGlobalUser(int pipe)
        {
            return this.Call<int, SteamClient009.NativeConnectToGlobalUser>(this.Functions.ConnectToGlobalUser, (object)this.ObjectAddress, (object)pipe);
        }

        // Kullanıcıyı serbest bırakır
        public void ReleaseUser(int pipe, int user)
        {
            this.Call<SteamClient009.NativeReleaseUser>(this.Functions.ReleaseUser, (object)this.ObjectAddress, (object)pipe, (object)user);
        }

        // Yerel IP bağlantısını belirler
        public void SetLocalIPBinding(uint host, ushort port)
        {
            this.Call<SteamClient009.NativeSetLocalIPBinding>(this.Functions.SetLocalIPBinding, (object)this.ObjectAddress, (object)host, (object)port);
        }

        // ISteamUser arabirimini alır
        private TClass GetISteamUser<TClass>(int user, int pipe, string version) where TClass : INativeWrapper, new()
        {
            IntPtr objectAddress = this.Call<IntPtr, SteamClient009.NativeGetISteamUser>(this.Functions.GetISteamUser, (object)this.ObjectAddress, (object)user, (object)pipe, (object)version);
            TClass @class = new TClass();
            @class.SetupFunctions(objectAddress);
            return @class;
        }

        // SteamUser012 arabirimini alır
        public SteamUser012 GetSteamUser012(int user, int pipe)
        {
            return this.GetISteamUser<SteamUser012>(user, pipe, "SteamUser012");
        }

        // ISteamUserStats arabirimini alır
        private TClass GetISteamUserStats<TClass>(int user, int pipe, string version) where TClass : INativeWrapper, new()
        {
            IntPtr objectAddress = this.Call<IntPtr, SteamClient009.NativeGetISteamUserStats>(this.Functions.GetISteamUserStats, (object)this.ObjectAddress, (object)user, (object)pipe, (object)version);
            TClass @class = new TClass();
            @class.SetupFunctions(objectAddress);
            return @class;
        }

        // SteamUserStats007 arabirimini alır
        public SteamUserStats007 GetSteamUserStats006(int user, int pipe)
        {
            return this.GetISteamUserStats<SteamUserStats007>(user, pipe, "STEAMUSERSTATS_INTERFACE_VERSION007");
        }

        // ISteamUtils arabirimini alır
        public TClass GetISteamUtils<TClass>(int pipe, string version) where TClass : INativeWrapper, new()
        {
            IntPtr objectAddress = this.Call<IntPtr, SteamClient009.NativeGetISteamUtils>(this.Functions.GetISteamUtils, (object)this.ObjectAddress, (object)pipe, (object)version);
            TClass @class = new TClass();
            @class.SetupFunctions(objectAddress);
            return @class;
        }

        // ISteamApps arabirimini alır
        private TClass GetISteamApps<TClass>(int user, int pipe, string version) where TClass : INativeWrapper, new()
        {
            IntPtr objectAddress = this.Call<IntPtr, SteamClient009.NativeGetISteamApps>(this.Functions.GetISteamApps, (object)user, (object)pipe, (object)version);
            TClass @class = new TClass();
            @class.SetupFunctions(objectAddress);
            return @class;
        }

        // Belirli bir isme sahip genel ISteam arabirimini alır
        private TClass GetISteamGenericInterface<TClass>(int user, int pipe, string name) where TClass : INativeWrapper, new()
        {
            IntPtr objectAddress = this.Call<IntPtr, SteamClient009.NativeGetISteamGenericInterface>(this.Functions.GetISteamGenericInterface, (object)this.ObjectAddress, (object)user, (object)pipe, (object)name);
            TClass @class = new TClass();
            @class.SetupFunctions(objectAddress);
            return @class;
        }

        // Uyarı mesajı kancasını ayarlar
        public void SetWarningMessageHook(SteamClient009.SteamAPIWarningMessageHook hook)
        {
            this.Call<SteamClient009.NativeSetWarningMessageHook>(this.Functions.SetWarningMessageHook, (object)this.ObjectAddress, (object)hook);
        }

        // Steam boru hattı oluşturmak için kullanılan C++ işlevin temsil eden delegate
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int NativeCreateSteamPipe(IntPtr thisObject);

        // Steam boru hattını serbest bırakmak için kullanılan C++ işlevin temsil eden delegate
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool NativeReleaseSteamPipe(IntPtr thisobj, Int32 hSteamPipe);
        [return: MarshalAs(UnmanagedType.I1)]

        // Yerel kullanıcı oluşturmak ve bir boru hattına bağlamak için kullanılan C++ işlevin temsil eden delegate
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int NativeCreateLocalUser(IntPtr thisObject, ref int pipe, AccountType type);

        // Küresel Steam kullanıcısına bir boru hattıyla bağlanmak için kullanılan C++ işlevin temsil eden delegate
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int NativeConnectToGlobalUser(IntPtr thisObject, int pipe);

        // Kullanıcıyı serbest bırakmak için kullanılan C++ işlevin temsil eden delegate
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeReleaseUser(IntPtr thisObject, int pipe, int user);

        // Yerel IP bağlantısını belirlemek için kullanılan C++ işlevin temsil eden delegate
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeSetLocalIPBinding(IntPtr thisObject, uint host, ushort port);

        // ISteamUser arabirimini almak için kullanılan C++ işlevin temsil eden delegate
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr NativeGetISteamUser(IntPtr thisObject, int hSteamUser, int hSteamPipe, string pchVersion);

        // ISteamGameServer arabirimini almak için kullanılan C++ işlevin temsil eden delegate
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr NativeGetISteamGameServer(IntPtr thisObject, int hSteamUser, int hSteamPipe, string pchVersion);

        // ISteamUserStats arabirimini almak için kullanılan C++ işlevin temsil eden delegate
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr NativeGetISteamUserStats(IntPtr thisObject, int hSteamUser, int hSteamPipe, string pchVersion);

        // ISteamUtils arabirimini almak için kullanılan C++ işlevin temsil eden delegate
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr NativeGetISteamUtils(IntPtr thisObject, int hSteamPipe, string pchVersion);

        // ISteamApps arabirimini almak için kullanılan C++ işlevin temsil eden delegate
        private delegate IntPtr NativeGetISteamApps(int hSteamUser, int hSteamPipe, string pchVersion);

        // Belirli bir isme sahip genel ISteam arabirimini almak için kullanılan C++ işlevin temsil eden delegate
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr NativeGetISteamGenericInterface(IntPtr thisObject, int hSteamUser, int hSteamPipe, string pchVersion);

        // Steam API uyarı mesajı kancası için kullanılan delegate
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate void SteamAPIWarningMessageHook(int pipe, string message);

        // Uyarı mesajı kancasını ayarlamak için kullanılan C++ işlevin temsil eden delegate
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr NativeSetWarningMessageHook(IntPtr thisObject, SteamClient009.SteamAPIWarningMessageHook hook);
    }
}
