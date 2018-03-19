using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineStore.Core.Contracts;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

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

        public string ValidateValue(string property, bool isRequired)
        {
            if (isRequired)
            {
                property = property != string.Empty ? property : throw new ArgumentException("The field is Required");
            }
            else
            {
                property = property != string.Empty ? property : null;
            }

            return property;
        }

        public void ValidateEmail(string email)
        {
            try
            {
                new MailAddress(email);
            }
            catch
            {
                throw new ArgumentException("Wrong email format!");
            }

        }

        public void ValidatePassword(string password)
        {
            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";

            if (!Regex.IsMatch(password, passwordPattern))
            {
                throw new ArgumentException("Password must be minimum eight characters, at least one letter and one number!");
            }
        }

        public void ValidateLength(string property, int minLength, int maxLength)
        {
            int propLength = property.Length;

            if (propLength < minLength || propLength > maxLength)
            {
                throw new ArgumentException($"Field length must be between {minLength} and {maxLength} symbols!");
            }
        }

        public void ValidateLength(int property, int minLength, int maxLength)
        {
            if (property < minLength || property > maxLength)
            {
                throw new ArgumentException($"Field length must be between {minLength} and {maxLength}!");
            }
        }
    }
}
