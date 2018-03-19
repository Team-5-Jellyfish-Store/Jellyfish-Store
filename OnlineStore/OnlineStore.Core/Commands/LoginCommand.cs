using OnlineStore.Core.Contracts;
using OnlineStore.Data.Contracts;
using System;
using System.Linq;

namespace OnlineStore.Core.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IOnlineStoreContext context;
        private readonly IUserSessionService userSession;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IHasher hasher;

        public LoginCommand(IOnlineStoreContext context, IUserSessionService userSession, IWriter writer, IReader reader, IHasher hasher)
        {
            this.context = context ?? throw new System.ArgumentNullException(nameof(context));
            this.userSession = userSession ?? throw new System.ArgumentNullException(nameof(userSession));
            this.writer = writer ?? throw new System.ArgumentNullException(nameof(writer));
            this.reader = reader ?? throw new System.ArgumentNullException(nameof(reader));
            this.hasher = hasher ?? throw new System.ArgumentNullException(nameof(hasher));
        }

        public string ExecuteThisCommand()
        {
            var username = string.Empty;
            var password = string.Empty;

            this.writer.Write("Username: ");
            username = this.reader.Read();

            this.writer.Write("Password: ");
            password = this.reader.Read();

            var user = context.Users.SingleOrDefault(x => x.Username == username);

            if (user == null)
            {
                throw new ArgumentException("User with that username don't exist!");
            }

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
