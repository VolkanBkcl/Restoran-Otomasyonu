using FluentValidation;
using RestoranOtomasyonu.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOtomasyonu.Entities.Validations
{
    /// <summary>
    /// Siparisler entity için FluentValidation kuralları
    /// </summary>
    public class SiparislerValidator : AbstractValidator<Siparisler>
    {
        public SiparislerValidator()
        {
            RuleFor(x => x.MasaId)
                .GreaterThan(0).WithMessage("Masa seçimi zorunludur.");

            RuleFor(x => x.KullaniciId)
                .GreaterThan(0).WithMessage("Kullanıcı seçimi zorunludur.");

            RuleFor(x => x.Tutar)
                .GreaterThanOrEqualTo(0).WithMessage("Tutar 0'dan küçük olamaz.");

            RuleFor(x => x.IndirimOrani)
                .InclusiveBetween(0, 100).WithMessage("İndirim oranı 0-100 arasında olmalıdır.");

            RuleFor(x => x.NetTutar)
                .GreaterThanOrEqualTo(0).WithMessage("Net tutar 0'dan küçük olamaz.");

            RuleFor(x => x.SatisKodu)
                .MaximumLength(15).WithMessage("Satış kodu en fazla 15 karakter olabilir.");

            RuleFor(x => x.Aciklama)
                .MaximumLength(300).WithMessage("Açıklama en fazla 300 karakter olabilir.");
        }
    }
}

