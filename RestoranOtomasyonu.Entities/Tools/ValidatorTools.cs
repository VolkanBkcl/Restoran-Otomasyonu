using FluentValidation;
using RestoranOtomasyonu.Entities.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.Entities.Tools
{
    public class ValidatorTools
    {
        public static bool Validates(IValidator validator, IEntity entity)
        {

            bool result = true;
            ValidationContext<IEntity> context = new ValidationContext<IEntity>(entity);
            var validationResult = validator.Validate(context);
            if (!validationResult.IsValid)
            {
                string message = null;
                foreach (var error in validationResult.Errors)
                {
                    message += error.ErrorMessage + System.Environment.NewLine;
                }
                MessageBox.Show(message);
                result = false;
            }
            return result;
        }
    }
}
