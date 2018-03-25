using OnlineStore.DTO.MappingContracts;
using OnlineStore.Models.DataModels;
using OnlineStore.Models.Enums;
using System;
using System.Text.RegularExpressions;

namespace OnlineStore.DTO.UserModels
{
    public class UserRegisterModel : IMapTo<User>
    {
        private readonly string emailPattern = @"^\S+@\S+$";

        private string username;
        private string email;
        private string password;
        private string firstName;
        private string lastName;
        private string addressText;
        private string townName;
        private UserRole role;

        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException("Username is Required");
                }

                if (value.Length < 5 || value.Length > 20)
                {
                    throw new ArgumentException("Username must be between 5 and 20 characters long!");
                }

                this.username = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        public string EMail
        {
            get
            {
                return this.email;
            }
            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException("Email is Required");
                }

                if (!Regex.IsMatch(value, emailPattern))
                {
                    throw new ArgumentException("Invalid email!");
                }

                this.email = value;
            }
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                if (value == string.Empty)
                {
                    value = null;
                }

                this.firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                if (value == string.Empty)
                {
                    value = null;
                }

                this.lastName = value;
            }
        }

        public string AddressText
        {
            get
            {
                return this.addressText;
            }
            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException("Address is Required");
                }

                this.addressText = value;
            }
        }

        public string TownName
        {
            get
            {
                return this.townName;
            }
            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException("Town is Required");
                }

                this.townName = value;
            }
        }

        public UserRole Role
        {
            get
            {
                return this.role;
            }
            set
            {
                this.role = value;
            }
        }
    }
}
