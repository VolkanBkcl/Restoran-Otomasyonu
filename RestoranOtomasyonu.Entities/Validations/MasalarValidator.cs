using FluentValidation;
using RestoranOtomasyonu.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOtomasyonu.Entities.Validations
{
    public class MasalarValidator:AbstractValidator<Masalar>
    {
        public MasalarValidator()
        {
            RuleFor(p => p.MasaAdi).NotEmpty().WithMessage("Masa Adı alanı boş geçilemez!");
        }
    }
}
