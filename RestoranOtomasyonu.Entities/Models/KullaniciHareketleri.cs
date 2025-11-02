using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOtomasyonu.Entities.Models
{
    
    public class KullaniciHareketleri
    {
        public int Id { get; set; }

        public int KullaniciId { get; set; }


        public string Aciklama { get; set; }


        public DateTime Tarih { get; set; }

        public Kullanicilar Kullanicilar { get; set; }

    }
}
