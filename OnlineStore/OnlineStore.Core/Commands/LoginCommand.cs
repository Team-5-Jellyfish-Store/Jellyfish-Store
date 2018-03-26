using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;
using System;

namespace OnlineStore.Core.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly string UserLoggedInSuccessMessage = "User {0} logged in successfuly!";
        private readonly string UserAlreadyLoggedInFailMessage = "Logout first!";
        private readonly string IncorrectPasswordFailMessage = "Incorrect Password!";

        private readonly IUserService userService;
        private readonly IUserSession userSession;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IHasher hasher;

        public LoginCommand(IUserService userService, IUserSession userSession, IWriter writer, IReader reader, IHasher hasher)
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

            this.writer.Write("Username: ");
            string username = this.reader.Read();

            this.writer.Write("Password: ");
            string password = this.reader.Read();

            var user = this.userService.GetRegisteredUser(username);
            var actualPassword = user.Password;

            if (!this.hasher.CheckPassword(password, actualPassword))
            {
                throw new ArgumentException(this.IncorrectPasswordFailMessage);
            }

            this.userSession.Login(user);

            return string.Format(this.UserLoggedInSuccessMessage, username);
        }
    }
}
