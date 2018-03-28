using OnlineStore.DTO.MappingContracts;
using OnlineStore.Models.DataModels;
using OnlineStore.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OnlineStore.DTO.UserModels
{
    public class UserRegisterModel : IMapTo<User>
    {
        private const string emailPattern = @"^\S+@\S+$";

        [Required(ErrorMessage = "Username is Required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 20 characters long!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [StringLength(30, MinimumLength = 3)]
        [RegularExpression(emailPattern, ErrorMessage = "Invalid email!")]
        public string EMail { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        public string AddressText { get; set; }

        [Required(ErrorMessage = "Town is Required")]
        public string TownName { get; set; }

        public UserRole Role { get; set; }
    }
}
