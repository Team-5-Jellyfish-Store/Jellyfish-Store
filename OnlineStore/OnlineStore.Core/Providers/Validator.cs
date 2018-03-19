using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineStore.Core.Contracts;


namespace OnlineStore.Core.Providers
{
    public class Validator : IValidator
    {
        public bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj); // System.Components.Data.Annotations
            var validationResults = new List<ValidationResult>();

            var isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return isValid;
        }
    }
}
