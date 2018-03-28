using AutoMapper;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO.UserModels;
using OnlineStore.DTO.UserModels.Contracts;
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
        private readonly ITownService townService;
        private readonly IAddressService addressService;

        public UserService(IOnlineStoreContext context, IMapper mapper, ITownService townService, IAddressService addressService)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
            this.addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
        }

        public void RegisterUser(IUserRegisterModel userModel)
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

            var town = this.context.Towns.SingleOrDefault(x => x.Name == userModel.TownName);

            if (town == null)
            {
                this.townService.Create(userModel.TownName);
                town = this.context.Towns.SingleOrDefault(x => x.Name == userModel.TownName);
            }

            var address = town.Addresses.FirstOrDefault(x => x.AddressText == userModel.AddressText);

            if (address == null)
            {
                this.addressService.Create(userModel.AddressText, userModel.TownName);
                address = town.Addresses.FirstOrDefault(x => x.AddressText == userModel.AddressText);

            }
            var userToRegister = this.mapper.Map<User>(userModel);

            userToRegister.Address = address;

            this.context.Users.Add(userToRegister);

            this.context.SaveChanges();
        }

        public IUserLoginModel GetRegisteredUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Username is required!", nameof(userName));
            }

            var userModel = this.context.Users.SingleOrDefault(x => x.Username == userName)
                ?? throw new ArgumentException($"User with username {userName} don't exist!");

            return this.mapper.Map<IUserLoginModel>(userModel);
        }
    }
}
