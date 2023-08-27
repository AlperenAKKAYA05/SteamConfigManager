using System; // Genel C# sistem kütüphanesi

namespace UserFetchSteam
{
    // Geri çağırım (callback) işlevselliğini sağlayan ICallback arabirimi
    public interface ICallback
    {
        int Id { get; } // Geri çağırım kimliği (ID)

        bool Server { get; } // Sunucu tarafında mı çalıştırılacağını belirten bayrak

        void Run(IntPtr param); // Geri çağırımın çalıştırılacağı metot, bir parametre alır
    }
}
