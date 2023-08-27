using System; // Genel C# sistem kütüphanesi
using System.Runtime.InteropServices; // Platforma özgü işlev çağrıları için kullanılan kütüphane

namespace UserFetchSteam
{
    // NativeClass yapısı, platforma özgü işlev çağrılarında kullanılan bir veri yapısıdır.
    // Bellek düzenlemesini temsil eden bir işaretçi içerir.
    [StructLayout(LayoutKind.Sequential, Pack = 1)] // Bellek düzenlemesi ve hizalama için ayarlamalar
    internal struct NativeClass
    {
        public IntPtr VirtualTable; // Sanal işlev tablosunu temsil eden işaretçi
    }
}
