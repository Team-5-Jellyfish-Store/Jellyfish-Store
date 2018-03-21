using AutoMapper;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
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
        private readonly IMapper mapper;

        public UserService(IOnlineStoreContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void RegisterUser(UserRegisterModel userModel)
        {
            if (userModel == null)
            {
                throw new ArgumentNullException(nameof(userModel));
            }

            if (this.context.Users.Any(x => x.Username == userModel.Username))
            {
                throw new ArgumentException("User with that username already exists!");
            }

            if (this.context.Users.Any(x => x.EMail == userModel.EMail))
            {
                throw new ArgumentException("User with that email already exists!");
            }

            var town = this.context.Towns.SingleOrDefault(x => x.Name == userModel.TownName)
                ?? throw new ArgumentException("Town not found!");
            var address = town.Addresses.FirstOrDefault(x => x.AddressText == userModel.AddressText)
                ?? throw new ArgumentException("Address not found!");

            var userToAdd = this.mapper.Map<User>(userModel);

            userToAdd.Address = address;

            //var userToAdd = new User()
            //{
            //    Username = userModel.Username,
            //    Password = userModel.Password,
            //    EMail = userModel.EMail,
            //    FirstName = userModel.FirstName,
            //    LastName = userModel.LastName,
            //    Address = address
            //};

            context.Users.Add(userToAdd);

            context.SaveChanges();
        }
        public UserRegisterModel GetUserWithUserName(string userName)
        {
            var userModel = context.Users.FirstOrDefault(x => x.Username == userName);

            if (userModel == null)
            {
                throw new ArgumentException("User with that username don't exist!");
            }

            return mapper.Map<UserRegisterModel>(userModel);
        }
    }
}
