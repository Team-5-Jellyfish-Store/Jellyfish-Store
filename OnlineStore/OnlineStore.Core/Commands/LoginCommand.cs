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
        private readonly IUserSession userSession;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IHasher hasher;
        private readonly IValidator validator;

        public LoginCommand(IUserService userService, IUserSession userSession, IWriter writer, IReader reader, IHasher hasher, IValidator validator)
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

            this.writer.Write("Password: ");
            string password = this.reader.Read();
            password = this.validator.ValidateValue(password, true);

            var user = this.userService.GetRegisteredUser(username);
            var actualPassword = user.Password;

            if (!this.hasher.CheckPassword(password, actualPassword))
            {
                throw new ArgumentException("Incorrect Password!");
            }

            this.userSession.Login(user);

            return $"User {username} logged in successfuly!";
        }
    }
}
