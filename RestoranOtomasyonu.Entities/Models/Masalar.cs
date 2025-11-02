using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOtomasyonu.Entities.Models
{
   
    public class Masalar
    {
        public int Id { get; set; }

        public string MasaAdi { get; set; }

        public string Aciklama { get; set; }
        

        public bool Durumu { get; set; }


        public bool RezervMi {  get; set; }


        public DateTime EklenmeTarihi { get; set; }


        public DateTime SonİslemTarihi { get; set; }


        public ICollection<MasaHareketleri> MasaHareketleri { get; set; }

    }
}
