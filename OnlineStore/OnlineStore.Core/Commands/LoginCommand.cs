using OnlineStore.Core.Contracts;
using OnlineStore.Data.Contracts;
using System.Linq;

namespace OnlineStore.Core.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IOnlineStoreContext context;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IHasher hasher;

        public LoginCommand(IOnlineStoreContext context, IWriter writer, IReader reader, IHasher hasher)
        {
            this.context = context ?? throw new System.ArgumentNullException(nameof(context));
            this.writer = writer ?? throw new System.ArgumentNullException(nameof(writer));
            this.reader = reader ?? throw new System.ArgumentNullException(nameof(reader));
            this.hasher = hasher ?? throw new System.ArgumentNullException(nameof(hasher));
        }

        public string ExecuteThisCommand()
        {
            var usernames = context.Users.Select(x => x.Username).ToList();

            var username = string.Empty;
            var password = string.Empty;

            do
            {
                this.writer.Write("Username: ");
                username = this.reader.Read();

                this.writer.Write("Password: ");
                password = this.reader.Read();

                if (!usernames.Exists(x => x == username))
                {
                    username = string.Empty;
                    this.writer.WriteLine("User with that username don't exist!");
                }
                else
                {
                    var actualPassword = context.Users.Where(x => x.Username == username).Select(x => x.Password).FirstOrDefault();

                    if (!this.hasher.CheckPassword(password, actualPassword))
                    {
                        username = string.Empty;
                        this.writer.WriteLine("Incorrect Password!");
                    }
                }
            } while (username == string.Empty);

            return $"User {username} logged in successfuly!";
        }
    }
}
