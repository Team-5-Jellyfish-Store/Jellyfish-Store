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
        private readonly IValidator validator;

        public LoginCommand(IOnlineStoreContext context, IUserSessionService userSession, IWriter writer, IReader reader, IHasher hasher, IValidator validator)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
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

            this.writer.Write("Password: ");
            string password = this.reader.Read();
            password = this.validator.ValidateValue(password, true);

            var user = this.context.Users.SingleOrDefault(x => x.Username == username);
            var actualPassword = user.Password;

            if (user == null)
            {
                throw new ArgumentException("User with that username don't exist!");
            }

            if (!this.hasher.CheckPassword(password, actualPassword))
            {
                throw new ArgumentException("Incorrect Password!");
            }

            this.userSession.SetLoggedUser(user);

            return $"User {username} logged in successfuly!";
        }


    }
}
