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
            this.writer.Write("Username: ");
            string username = this.reader.Read();
            username = ValidateValue(username, true);

            this.writer.Write("Password: ");
            string password = this.reader.Read();
            password = ValidateValue(password, true);

            var user = context.Users.SingleOrDefault(x => x.Username == username);
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

        private string ValidateValue(string property, bool isRequired)
        {
            if (isRequired)
            {
                property = property != string.Empty ? property : throw new ArgumentException("The field is Required");
            }
            else
            {
                property = property != string.Empty ? property : null;
            }

            return property;
        }
    }
}
