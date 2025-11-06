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
    public class MenuDal:EntityRepositoryBase<RestoranContext,Menu,MenuValidator>
    {
    }
}
