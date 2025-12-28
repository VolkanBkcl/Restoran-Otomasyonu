using FluentValidation;
using FluentValidation.Results;
using RestoranOtomasyonu.Entities.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOtomasyonu.Entities.Tools
{
    public class ValidatorTools
    {
        public static bool Validates<TEntity>(FluentValidation.IValidator<TEntity> validator, TEntity entity) where TEntity : IEntity
        {
            ValidationContext<TEntity> context = new ValidationContext<TEntity>(entity);
            var validationResult = validator.Validate(context);
            return validationResult.IsValid;
        }

        public static string GetValidationErrors<TEntity>(FluentValidation.IValidator<TEntity> validator, TEntity entity) where TEntity : IEntity
        {
            ValidationContext<TEntity> context = new ValidationContext<TEntity>(entity);
            var validationResult = validator.Validate(context);
            if (!validationResult.IsValid)
            {
                StringBuilder message = new StringBuilder();
                foreach (var error in validationResult.Errors)
                {
                    message.AppendLine(error.ErrorMessage);
                }
                return message.ToString();
            }
            return null;
        }
    }
}
