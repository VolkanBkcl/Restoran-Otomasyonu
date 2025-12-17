using System;

namespace RestoranOtomasyonu.Entities.Enums
{
    /// <summary>
    /// Masa ve sipariş akışını daha detaylı izlemek için kullanılan durum enum'u.
    /// Mevcut <see cref="SiparisDurumu"/> ve <see cref="OdemeDurumu"/> alanlarının
    /// birleşik, kullanıcıya gösterilen tek bir mantıksal durumunu temsil eder.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Masada aktif sipariş yok / tamamen boş.
        /// </summary>
        Bos = 0,

        /// <summary>
        /// Sipariş alındı ancak henüz mutfakta hazırlanmaya başlanmadı.
        /// </summary>
        SiparisAlindi = 1,

        /// <summary>
        /// Sipariş mutfakta hazırlanıyor.
        /// </summary>
        Hazirlaniyor = 2,

        /// <summary>
        /// Sipariş masaya servis edildi.
        /// </summary>
        ServisEdildi = 3,

        /// <summary>
        /// Müşteri hesabı istedi, ödeme bekleniyor.
        /// </summary>
        OdemeBekleniyor = 4,

        /// <summary>
        /// Ödeme tamamlandı (nakit veya blockchain).
        /// </summary>
        Odendi = 5
    }
}

