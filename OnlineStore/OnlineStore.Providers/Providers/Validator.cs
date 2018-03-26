using OnlineStore.Providers.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Providers.Providers
{
    public class Validator : IValidator
    {
        public bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return isValid;
        }
    }
}
