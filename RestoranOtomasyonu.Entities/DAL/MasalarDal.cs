using RestoranOtomasyonu.Entities.Models;
using RestoranOtomasyonu.Entities.Repository;
using RestoranOtomasyonu.Entities.Validations;

namespace RestoranOtomasyonu.Entities.DAL
{
    public class MasalarDal : EntityRepositoryBase<RestoranContext, Masalar, MasalarValidator>
    {
    }
}
