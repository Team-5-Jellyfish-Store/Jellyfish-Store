using OnlineStore.Core.Security;
using OnlineStore.Data;
using OnlineStore.Data.Contracts;
using OnlineStore.Models.DataModels;
using System;
using System.Linq;
using OnlineStore.Core.Contracts;
using OnlineStore.Models.DataModels;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace OnlineStore.Core.Commands
{
    public class RegisterClientCommand : ICommand
    {
        private readonly IOnlineStoreContext context;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IHasher hasher;

        public RegisterClientCommand(IOnlineStoreContext context, IWriter writer, IReader reader, IHasher hasher)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        public string ExecuteThisCommand()
        {
            this.writer.Write("Username: ");
            string username = this.reader.Read();
            username = ValidateValue(username, true);

            this.writer.Write("Email: ");
            string email = this.reader.Read();
            email = ValidateValue(email, true);
            ValidateEmail(email);

            this.writer.Write("Password: ");
            string password = this.reader.Read();
            password = ValidateValue(password, true);
            ValidateLength(password, 8, 30);
            ValidatePassword(password);

            this.writer.Write("Confirm password: ");
            string confirmedPassword = this.reader.Read();
            confirmedPassword = ValidateValue(confirmedPassword, true);
            ValidateLength(confirmedPassword, 8, 30);

            this.writer.Write("First Name: ");
            string firstName = this.reader.Read();
            firstName = ValidateValue(firstName, false);

            this.writer.Write("Last Name: ");
            string lastName = this.reader.Read();
            lastName = ValidateValue(lastName, false);

            this.writer.Write("Address: ");
            string address = this.reader.Read();
            address = ValidateValue(address, true);

            if (this.context.Users.Any(x => x.Username == username))
            {
                throw new ArgumentException("User with that username already exists!");
            }

            if (password != confirmedPassword)
            {
                throw new ArgumentException("Password not matching!");
            }

            password = this.hasher.CreatePassword(password);

            var addressFromDb = context.Addresses.Where(x => x.AddressText == address).FirstOrDefault()
                                ?? throw new ArgumentNullException("Address not Found!");

            var newUser = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                EMail = email,
                Password = password,
                AddressId = addressFromDb.Id
            };

            context.Users.Add(newUser);

            context.SaveChanges();

            return "User registered successfully!";
        }

        private void ValidateEmail(string email)
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

        private void ValidatePassword(string password)
        {
            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";

            if (!Regex.IsMatch(password, passwordPattern))
            {
                throw new ArgumentException("Password must be minimum eight characters, at least one letter and one number!");
            }
        }

        private void ValidateLength(string property, int minLength, int maxLength)
        {
            int propLength = property.Length;

            if (propLength < minLength || propLength > maxLength)
            {
                throw new ArgumentException($"Field length must be between {minLength} and {maxLength} symbols!");
            }
        }

        private string ValidateValue(string property, bool isRequired)
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
    }
}
