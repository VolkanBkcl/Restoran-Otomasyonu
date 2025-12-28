using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOtomasyonu.Entities.Enums
{
    /// <summary>
    /// Sipariş durumu enum'u
    /// Mutfak ve kurye takibi için kullanılır
    /// </summary>
    public enum SiparisDurumu
    {
        /// <summary>
        /// Sipariş alındı (Onay bekliyor - eski değer ile uyumlu)
        /// </summary>
        SiparisAlindi = 0,
        OnayBekliyor = 0, // Eski değer ile uyumluluk için
        
        /// <summary>
        /// Sipariş hazırlanıyor
        /// </summary>
        Hazirlaniyor = 1,
        
        /// <summary>
        /// Sipariş hazır
        /// </summary>
        Hazir = 2,
        TeslimEdildi = 2, // Eski değer ile uyumluluk için
        
        /// <summary>
        /// Sipariş servis edildi
        /// </summary>
        ServisEdildi = 3,
        Tamamlandi = 3, // Eski değer ile uyumluluk için
        
        /// <summary>
        /// Sipariş iptal edildi
        /// </summary>
        IptalEdildi = 4
    }
}

