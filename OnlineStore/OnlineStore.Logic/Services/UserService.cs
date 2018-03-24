using AutoMapper;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.DTO.UserModels;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using System;
using System.Linq;

namespace OnlineStore.Logic.Services
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

            var userToRegister = this.mapper.Map<User>(userModel);
            userToRegister.Address = address;

            this.context.Users.Add(userToRegister);

            this.context.SaveChanges();
        }

        public UserLoginModel GetRegisteredUser(string userName)
        {
            var userModel = this.context.Users.SingleOrDefault(x => x.Username == userName)
                ?? throw new ArgumentException($"User with username {userName} don't exist!");

            return mapper.Map<UserLoginModel>(userModel);
        }
    }
}
