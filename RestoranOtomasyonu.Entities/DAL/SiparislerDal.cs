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
    /// <summary>
    /// Siparisler tablosu için Data Access Layer
    /// </summary>
    public class SiparislerDal : EntityRepositoryBase<RestoranContext, Siparisler, SiparislerValidator>
    {
    }
}

