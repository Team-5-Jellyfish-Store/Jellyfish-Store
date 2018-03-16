using OnlineStore.Core.Security;
using OnlineStore.Data;
using OnlineStore.Data.Contracts;
using OnlineStore.Models;
using System;
using System.Linq;

namespace OnlineStore.Core.Commands
{
    public class RegisterClientCommand : ICommand
    {
        private readonly IOnlineStoreContext context;
        private readonly IHasher hasher;

        public RegisterClientCommand(IOnlineStoreContext context, IHasher hasher)
        {
            this.context = context;
            this.hasher = hasher;
        }

        public void Execute(string[] parameters)
        {
            string username = parameters[0];
            string password = parameters[1];
            string confirmedPassword = parameters[2];
            string address = parameters[3];
            string firstName = parameters[4];
            string lastName = parameters[5];

            if (password != confirmedPassword)
            {
                throw new ArgumentException("Password not matching!");
            }

            var addressFromDb = context.Addresses.Where(x => x.AddressText == address).FirstOrDefault()
                                ?? throw new ArgumentNullException("Address not Found!");

            password = this.hasher.CreatePassword(password);

            var newUser = new Client()
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password,
                AddressId = addressFromDb.Id
            };

            context.Clients.Add(newUser);

            context.SaveChanges();
        }
    }
}
