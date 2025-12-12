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
        OnayBekliyor = 0,
        Hazirlaniyor = 1,
        TeslimEdildi = 2,
        Tamamlandi = 3,
        IptalEdildi = 4
    }
}

