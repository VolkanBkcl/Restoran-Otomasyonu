using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOtomasyonu.Entities.Enums
{
    /// <summary>
    /// Sipariş ödeme durumu enum'u
    /// Alman usulü (parçalı) ödeme sistemi için kullanılır
    /// </summary>
    public enum OdemeDurumu
    {
        /// <summary>
        /// Henüz ödenmedi
        /// </summary>
        Odenmedi = 0,

        /// <summary>
        /// Müşteri kendi payını ödedi (parçalı ödeme)
        /// </summary>
        KendiOdedi = 1,

        /// <summary>
        /// Masadaki tüm siparişler ödendi (toplu ödeme)
        /// </summary>
        TumuOdendi = 2
    }
}

