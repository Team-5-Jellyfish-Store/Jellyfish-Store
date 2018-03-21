using OnlineStore.Core.Contracts;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic.Contracts;
using System;
using System.Linq;

namespace OnlineStore.Core.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly IUserSessionService userSession;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IHasher hasher;
        private readonly IValidator validator;

        public LoginCommand(IUserService userService, IUserSessionService userSession, IWriter writer, IReader reader, IHasher hasher, IValidator validator)
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

            this.writer.Write("Password: ");
            string password = this.reader.Read();
            password = this.validator.ValidateValue(password, true);

            var user = userService.GetUserWithUserName(username);
            var actualPassword = user.Password;

            if (!this.hasher.CheckPassword(password, actualPassword))
            {
                throw new ArgumentException("Incorrect Password!");
            }

            this.userSession.SetLoggedUser(user);

            return $"User {username} logged in successfuly!";
        }
    }
}
