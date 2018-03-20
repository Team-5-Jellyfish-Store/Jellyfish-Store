using System;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Core.Commands
{
    public class RegisterUserCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly IUserSessionService userSession;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IHasher hasher;
        private readonly IValidator validator;

        public RegisterUserCommand(IUserService userService, IUserSessionService userSession, IWriter writer, IReader reader, IHasher hasher, IValidator validator)
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
            var loggedUser = this.userSession.GetLoggedUser();

            if (loggedUser != null)
            {
                throw new ArgumentException($"User {loggedUser} is logged in!");
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

            this.writer.Write("Address: ");
            string address = this.reader.Read();
            address = this.validator.ValidateValue(address, true);

            if (password != confirmedPassword)
            {
                throw new ArgumentException("Password not matching!");
            }

            password = this.hasher.CreatePassword(password);

            this.userService.RegisterUser(username, password, email, firstName, lastName, address);

            return "User registered successfully!";
        }
    }
}
