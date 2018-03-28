using System;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.DTO;
using OnlineStore.DTO.UserModels;
using System.Text.RegularExpressions;
using OnlineStore.DTO.Factory;
using OnlineStore.Providers.Contracts;

namespace OnlineStore.Core.Commands
{
    public class RegisterUserCommand : ICommand
    {
        private readonly string UserRegisteredSuccessMessage = "User {0} registered successfully!";
        private readonly string UserAlreadyLoggedInFailMessage = "Logout first!";
        private readonly string PasswordsNotMathingFailMessage = "Password not matching!";
        private readonly string InvalidValuesInModelFailMessage = "Invalid model! Please provide valid values!";

        private readonly IUserService userService;
        private readonly IUserSession userSession;
        private readonly IDataTransferObjectFactory dataTransferObjectFactory;
        private readonly IValidator validator;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IHasher hasher;

        public RegisterUserCommand(IUserService userService, IUserSession userSession, IDataTransferObjectFactory dataTransferObjectFactory, IValidator validator, IWriter writer, IReader reader, IHasher hasher)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
            this.dataTransferObjectFactory = dataTransferObjectFactory ?? throw new ArgumentNullException(nameof(dataTransferObjectFactory));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        public string ExecuteThisCommand()
        {
            if (this.userSession.HasSomeoneLogged())
            {
                throw new ArgumentException(this.UserAlreadyLoggedInFailMessage);
            }

            this.writer.Write("Username: ");
            string username = this.reader.Read();

            this.writer.Write("Email: ");
            string email = this.reader.Read();

            this.writer.Write("Password: ");
            string password = this.reader.Read();
            this.hasher.ValidatePassword(password);

            this.writer.Write("Confirm password: ");
            string confirmedPassword = this.reader.Read();

            if (password != confirmedPassword)
            {
                throw new ArgumentException(this.PasswordsNotMathingFailMessage);
            }

            password = this.hasher.CreatePassword(password);

            this.writer.Write("First Name: ");
            string firstName = this.reader.Read();

            this.writer.Write("Last Name: ");
            string lastName = this.reader.Read();

            this.writer.Write("Town: ");
            string townName = this.reader.Read();

            this.writer.Write("Address: ");
            string addressText = this.reader.Read();

            var userModel = this.dataTransferObjectFactory.CreateUserRegisterModel(username, email, password, firstName, lastName, townName, addressText);

            if (!this.validator.IsValid(userModel))
            {
                throw new ArgumentException(this.InvalidValuesInModelFailMessage);
            }

            this.userService.RegisterUser(userModel);

            return string.Format(this.UserRegisteredSuccessMessage, username);
        }
    }
}
