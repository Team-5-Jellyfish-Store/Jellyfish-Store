﻿using OnlineStore.DTO.MappingContracts;
using OnlineStore.DTO.UserModels.Contracts;
using OnlineStore.Models.DataModels;
using OnlineStore.Models.Enums;

namespace OnlineStore.DTO.UserModels
{
    public class UserLoginModel : IUserLoginModel, IMapFrom<User>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
