using OnlineStore.Core.Security;
using OnlineStore.Data;
using OnlineStore.Data.Contracts;
using OnlineStore.Models;
using System;
using System.Linq;
using OnlineStore.Core.Contracts;
using OnlineStore.Models.DataModels;

namespace OnlineStore.Core.Commands
{
    public class RegisterClientCommand : ICommand
    {
        private readonly IOnlineStoreContext context;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IHasher hasher;

        public RegisterClientCommand(IOnlineStoreContext context, IWriter writer, IReader reader, IHasher hasher)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        public string ExecuteThisCommand()
        {
            this.writer.Write("Username: ");
            string username = this.reader.Read();
            username = username != string.Empty ? username : throw new ArgumentException("Username is Required");

            if (this.context.Users.Any(x => x.Username == username))
            {
                throw new ArgumentException("User with that username already exists!");
            }

            this.writer.Write("Password: ");
            string password = this.reader.Read();
            password = password != string.Empty ? password : throw new ArgumentException("Password is Required");

            this.writer.Write("Confirm password: ");
            string confirmedPassword = this.reader.Read();

            if (password != confirmedPassword)
            {
                throw new ArgumentException("Password not matching!");
            }

            this.writer.Write("First Name: ");
            string firstName = this.reader.Read();

            this.writer.Write("Last Name: ");
            string lastName = this.reader.Read();

            this.writer.Write("Address: ");
            string address = this.reader.Read();
            address = address != string.Empty ? address : throw new ArgumentException("Address is Required");

            var addressFromDb = context.Addresses.Where(x => x.AddressText == address).FirstOrDefault()
                                ?? throw new ArgumentNullException("Address not Found!");

            password = this.hasher.CreatePassword(password);

            var newUser = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password,
                AddressId = addressFromDb.Id
            };

            context.Users.Add(newUser);

            context.SaveChanges();

            return "User registered successfully!";
        }
    }
}
