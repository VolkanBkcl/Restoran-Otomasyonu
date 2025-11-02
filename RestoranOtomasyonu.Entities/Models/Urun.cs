using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOtomasyonu.Entities.Models
{
    
    public class Urun
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Menu")]
        public int MenuId { get; set; }

       
        public string MasaAdi { get; set; }

        public string UrunAdi { get; set; }

        public decimal BirimFiyati1 { get; set; }

        public decimal BirimFiyati2 { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }


        public Menu Menu { get; set; }
    }
}
