using FluentValidation;
using RestoranOtomasyonu.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOtomasyonu.Entities.Validations
{
    public class SatislarValidator:AbstractValidator<Satislar>
    {
        public SatislarValidator()
        {
            RuleFor(p => p.SatisKodu).NotEmpty().WithMessage("Satış Kodu Adı alanı boş geçilemez!");
        }
    }
}
