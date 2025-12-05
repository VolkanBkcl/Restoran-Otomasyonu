using RestoranOtomasyonu.Entities.Models;
using RestoranOtomasyonu.Entities.Repository;
using RestoranOtomasyonu.Entities.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOtomasyonu.Entities.DAL
{
    public class UrunDal:EntityRepositoryBase<RestoranContext,Urun,UrunValidator>
    {
        public object UrunListele(RestoranContext context)
        {
            var liste = (from u in context.Urun
                         select new
                         {
                             u.Id,
                             u.MenuId,
                             Menu=u.Menu.MenuAdi,
                             u.UrunKodu,
                             u.UrunAdi,
                             u.BirimFiyati1,
                             u.BirimFiyati2,
                             u.BirimFiyati3,
                             u.Aciklama,
                             u.Resim,
                             u.Tarih

                         }).ToList();
            return liste;
        }
    }
}
