﻿using OnlineStore.Models.DataModels;

namespace OnlineStore.DTO
{
    public class UserRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public Address Address { get; set; }
    }
}
