using System; // Genel C# sistem kütüphanesi

namespace UserFetchSteam
{
    // INativeWrapper arabirimi, sarmalayıcı (wrapper) sınıflar için işlevlerin yapılandırılmasını sağlar
    public interface INativeWrapper
    {
        // Sarmalayıcı sınıfların işlevleri yapılandırmasını sağlayan metot
        void SetupFunctions(IntPtr objectAddress);
    }
}
