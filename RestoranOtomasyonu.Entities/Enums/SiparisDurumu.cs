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
        /// Sipariş alındı, beklemede
        /// </summary>
        Beklemede = 0,

        /// <summary>
        /// Mutfakta hazırlanıyor
        /// </summary>
        Hazirlaniyor = 1,

        /// <summary>
        /// Hazır, teslim edilmeyi bekliyor
        /// </summary>
        Hazir = 2,

        /// <summary>
        /// Teslim edildi
        /// </summary>
        TeslimEdildi = 3,

        /// <summary>
        /// İptal edildi
        /// </summary>
        Iptal = 4
    }
}

