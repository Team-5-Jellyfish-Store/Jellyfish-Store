using OnlineStore.Data.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.Logic
{
    public class UserService : IUserService
    {
        private readonly IOnlineStoreContext context;

        public UserService(IOnlineStoreContext context)
        {
            this.context = context;
        }

        public void RegisterUser(string username, string password, string email, string firstName, string lastName, string addressText)
        {
            if (this.context.Users.Any(x => x.Username == username))
            {
                throw new ArgumentException("User with that username already exists!");
            }

            if (this.context.Users.Any(x => x.EMail == email))
            {
                throw new ArgumentException("User with that email already exists!");
            }

            var addressFromDb = this.context.Addresses.Where(x => x.AddressText == addressText).FirstOrDefault()
                                ?? throw new ArgumentNullException("Address not Found!");

            var userToAdd = new User()
            {
                Username = username,
                Password = password,
                EMail = email,
                FirstName = firstName,
                LastName = lastName,
                Address = addressFromDb
            };

            context.Users.Add(userToAdd);

            context.SaveChanges();
        }
    }
}
