using RestoranOtomasyonu.Entities.Intefaces;
using RestoranOtomasyonu.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOtomasyonu.Entities.Models
{
    /// <summary>
    /// Siparişler tablosu - Mobil entegrasyon ve Alman usulü ödeme için
    /// </summary>
    public class Siparisler : IEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// Hangi masaya ait sipariş
        /// </summary>
        public int MasaId { get; set; }

        /// <summary>
        /// Hangi müşteri sipariş etti (KullaniciId)
        /// </summary>
        public int KullaniciId { get; set; }

        /// <summary>
        /// Satış kodu (MasaHareketleri ile ilişkilendirme için)
        /// </summary>
        public string SatisKodu { get; set; }

        /// <summary>
        /// Toplam tutar (indirim öncesi)
        /// </summary>
        public decimal Tutar { get; set; }

        /// <summary>
        /// İndirim oranı (yüzde)
        /// </summary>
        public decimal IndirimOrani { get; set; }

        /// <summary>
        /// Net tutar (indirim sonrası)
        /// </summary>
        public decimal NetTutar { get; set; }

        /// <summary>
        /// Ödeme durumu (Enum: Odenmedi, KendiOdedi, TumuOdendi)
        /// </summary>
        public OdemeDurumu OdemeDurumu { get; set; }

        /// <summary>
        /// Sipariş durumu (Enum: Beklemede, Hazirlaniyor, Hazir, TeslimEdildi, Iptal)
        /// </summary>
        public SiparisDurumu SiparisDurumu { get; set; }

        /// <summary>
        /// Açıklama/Not
        /// </summary>
        public string Aciklama { get; set; }

        /// <summary>
        /// Sipariş tarihi
        /// </summary>
        public DateTime Tarih { get; set; }

        // Navigation Properties
        /// <summary>
        /// İlişkili masa
        /// </summary>
        public virtual Masalar Masalar { get; set; }

        /// <summary>
        /// İlişkili kullanıcı (müşteri)
        /// </summary>
        public virtual Kullanicilar Kullanicilar { get; set; }
    }
}

