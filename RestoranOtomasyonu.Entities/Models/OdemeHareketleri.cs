using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOtomasyonu.Entities.Models
{
    
    public class OdemeHareketleri
    {
        public int Id { get; set; }


        public string SatisKodu { get; set; }


        public string OdemeTuru { get; set; }


        public decimal Odenen { get; set; } 


        public string Aciklama { get; set; }    


        public DateTime Tarih { get; set; }



    }
}
