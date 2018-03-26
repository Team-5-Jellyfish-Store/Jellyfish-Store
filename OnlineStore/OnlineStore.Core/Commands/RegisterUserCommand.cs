using System;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.DTO;
using OnlineStore.DTO.UserModels;
using System.Text.RegularExpressions;

namespace OnlineStore.Core.Commands
{
    public class RegisterUserCommand : ICommand
    {
        private readonly string UserRegisteredSuccessMessage = "User {0} registered successfully!";
        private readonly string UserAlreadyLoggedInFailMessage = "Logout first!";
        private readonly string PasswordsNotMathingFailMessage = "Password not matching!";

        private readonly IUserService userService;
        private readonly IUserSession userSession;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IHasher hasher;

        public RegisterUserCommand(IUserService userService, IUserSession userSession, IWriter writer, IReader reader, IHasher hasher)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
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

            var userModel = new UserRegisterModel();

            this.writer.Write("Username: ");
            string username = this.reader.Read();
            userModel.Username = username;

            this.writer.Write("Email: ");
            string email = this.reader.Read();
            userModel.EMail = email;

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

            userModel.Password = password;

            this.writer.Write("First Name: ");
            string firstName = this.reader.Read();
            userModel.FirstName = firstName;

            this.writer.Write("Last Name: ");
            string lastName = this.reader.Read();
            userModel.LastName = lastName;

            this.writer.Write("Town: ");
            string townName = this.reader.Read();
            userModel.TownName = townName;

            this.writer.Write("Address: ");
            string addressText = this.reader.Read();
            userModel.AddressText = addressText;

            this.userService.RegisterUser(userModel);

            return string.Format(this.UserRegisteredSuccessMessage, username);
        }
    }
}
