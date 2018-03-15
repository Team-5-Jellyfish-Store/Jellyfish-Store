﻿using OnlineStore.Data;
using OnlineStore.Data.Contracts;
using OnlineStore.Models;
using System;
using System.Linq;

namespace OnlineStore.Core.Commands
{
    public class RegisterClientCommand : ICommand
    {
        private readonly IOnlineStoreContext context;

        public RegisterClientCommand(IOnlineStoreContext context)
        {
            this.context = context;
        }

        public void Execute(string[] parameters)
        {
            string username = parameters[0];
            string password = parameters[1];
            string confirmedPassword = parameters[2];
            string townName = parameters[3];
            string firstName = parameters[4];
            string lastName = parameters[5];

            if (password != confirmedPassword)
            {
                throw new ArgumentException("Password not matching!");
            }

            var town = context.Towns.Where(x => x.Name == townName).FirstOrDefault()
                                ?? throw new ArgumentNullException();

            var newUser = new Client()
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password,
                TownId = town.Id
            };

            context.Clients.Add(newUser);

            context.SaveChanges();
        }
    }
}
