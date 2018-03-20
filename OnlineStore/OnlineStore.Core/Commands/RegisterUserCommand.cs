using OnlineStore.Data.Contracts;
using OnlineStore.Models.DataModels;
using System;
using System.Linq;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Core.Commands
{
    public class RegisterUserCommand : ICommand
    {
        private readonly IOnlineStoreContext context;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IHasher hasher;
        private readonly IValidator validator;

        public RegisterUserCommand(IOnlineStoreContext context, IWriter writer, IReader reader, IHasher hasher, IValidator validator)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public string ExecuteThisCommand()
        {
            this.writer.Write("Username: ");
            string username = this.reader.Read();
            username = this.validator.ValidateValue(username, true);

            this.writer.Write("Email: ");
            string email = this.reader.Read();
            email = this.validator.ValidateValue(email, true);
            this.validator.ValidateEmail(email);

            this.writer.Write("Password: ");
            string password = this.reader.Read();
            password = this.validator.ValidateValue(password, true);
            this.validator.ValidateLength(password, 8, 30);
            this.validator.ValidatePassword(password);

            this.writer.Write("Confirm password: ");
            string confirmedPassword = this.reader.Read();
            confirmedPassword = this.validator.ValidateValue(confirmedPassword, true);
            this.validator.ValidateLength(confirmedPassword, 8, 30);

            this.writer.Write("First Name: ");
            string firstName = this.reader.Read();
            firstName = this.validator.ValidateValue(firstName, false);

            this.writer.Write("Last Name: ");
            string lastName = this.reader.Read();
            lastName = this.validator.ValidateValue(lastName, false);

            this.writer.Write("Address: ");
            string address = this.reader.Read();
            address = this.validator.ValidateValue(address, true);

            if (this.context.Users.Any(x => x.Username == username))
            {
                throw new ArgumentException("User with that username already exists!");
            }

            if (password != confirmedPassword)
            {
                throw new ArgumentException("Password not matching!");
            }

            password = this.hasher.CreatePassword(password);

            var addressFromDb = this.context.Addresses.Where(x => x.AddressText == address).FirstOrDefault()
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
    }
}
