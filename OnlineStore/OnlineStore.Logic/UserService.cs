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

            var userToAdd = this.mapper.Map<User>(userModel);

            context.Users.Add(userToAdd);

            context.SaveChanges();
        }
    }
}
