using System; // Genel C# sistem kütüphanesi
using System.Collections.Generic; // Koleksiyon tiplerini kullanmak için gerekli kütüphane
using System.Runtime.InteropServices; // Platforma özgü işlev çağrıları için kullanılan kütüphane
using System.Linq.Expressions; // LINQ ifadeleri için kullanılan kütüphane

namespace UserFetchSteam
{
    // INativeWrapper arabirimini uygulayan soyut (abstract) NativeWrapper sınıfı
    // Platforma özgü işlev çağrılarını destekler ve işlevleri yapılandırır
    public abstract class NativeWrapper<TNativeFunctions> : INativeWrapper
    {
        private Dictionary<IntPtr, Delegate> FunctionCache = new Dictionary<IntPtr, Delegate>(); // İşlevlerin önbelleğini tutan sözlük
        protected IntPtr ObjectAddress; // Nesnenin bellek adresi
        protected TNativeFunctions Functions; // Platforma özgü işlevlerin arabirimi

        // Nesnenin dize temsilini oluşturan geçersiz kılınmış ToString() metodu
        public override string ToString()
        {
            return string.Format("Steam Interface<{0}> #{1:X8}", (object)typeof(TNativeFunctions), (object)this.ObjectAddress.ToInt32());
        }

        // INativeWrapper arabiriminden gelen SetupFunctions metodu
        // Nesnenin işlevleri yapılandırır ve bellek adresini ayarlar
        public void SetupFunctions(IntPtr objectAddress)
        {
            this.ObjectAddress = objectAddress;
            this.Functions = (TNativeFunctions)Marshal.PtrToStructure(((NativeClass)Marshal.PtrToStructure(this.ObjectAddress, typeof(NativeClass))).VirtualTable, typeof(TNativeFunctions));
        }

        // İşlev işaretçisi için temsilciyi önbelleğe almayı sağlayan metot
        protected Delegate GetDelegate<TDelegate>(IntPtr pointer)
        {
            Delegate forFunctionPointer;
            if (!this.FunctionCache.ContainsKey(pointer))
            {
                forFunctionPointer = Marshal.GetDelegateForFunctionPointer(pointer, typeof(TDelegate));
                this.FunctionCache[pointer] = forFunctionPointer;
            }
            else
                forFunctionPointer = this.FunctionCache[pointer];

            return forFunctionPointer;
        }

        // İşlev işaretçisini belirli bir temsilci türüne çeviren metot
        protected TDelegate GetFunction<TDelegate>(IntPtr pointer) where TDelegate : class
        {
            return (TDelegate)Convert.ChangeType(GetDelegate<TDelegate>(pointer), typeof(TDelegate));
        }

        // Belirli bir işlevi çağıran metot
        protected void Call<TDelegate>(IntPtr pointer, params object[] args)
        {
            this.GetDelegate<TDelegate>(pointer).DynamicInvoke(args);
        }

        // Belirli bir işlevi çağıran ve geri dönüş değeri alan metot
        protected TReturn Call<TReturn, TDelegate>(IntPtr pointer, params object[] args)
        {
            return (TReturn)this.GetDelegate<TDelegate>(pointer).DynamicInvoke(args);
        }
    }
}
