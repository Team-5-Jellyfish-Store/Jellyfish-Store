using System;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.DTO;
using OnlineStore.DTO.UserModels;

namespace OnlineStore.Core.Commands
{
    public class RegisterUserCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly IUserSession userSession;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IHasher hasher;
        private readonly IValidator validator;

        public RegisterUserCommand(IUserService userService, IUserSession userSession, IWriter writer, IReader reader, IHasher hasher, IValidator validator)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public string ExecuteThisCommand()
        {
            if (this.userSession.HasSomeoneLogged())
            {
                throw new ArgumentException($"Logout first!");
            }

            this.writer.Write("Username: ");
            string username = this.reader.Read();
            username = this.validator.ValidateValue(username, true);
            this.validator.ValidateLength(username, 5, 20);

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

            this.writer.Write("Town: ");
            string townName = this.reader.Read();
            townName = this.validator.ValidateValue(townName, true);

            this.writer.Write("Address: ");
            string addressText = this.reader.Read();
            addressText = this.validator.ValidateValue(addressText, true);

            if (password != confirmedPassword)
            {
                throw new ArgumentException("Password not matching!");
            }

            password = this.hasher.CreatePassword(password);

            var userModel = new UserRegisterModel()
            {
                Username = username,
                Password = password,
                EMail = email,
                FirstName = firstName,
                LastName = lastName,
                AddressText = addressText,
                TownName = townName
            };

            this.userService.RegisterUser(userModel);

            return "User registered successfully!";
        }
    }
}
