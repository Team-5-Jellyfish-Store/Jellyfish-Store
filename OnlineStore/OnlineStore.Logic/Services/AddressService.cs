using AutoMapper;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using System;
using System.Linq;

namespace OnlineStore.Logic.Services
{
    public class AddressService : IAddressService
    {
        private readonly IOnlineStoreContext context;

        public AddressService(IOnlineStoreContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create(string address, string townName)
        {
            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentException("Address is required!", nameof(address));
            }

            if (string.IsNullOrEmpty(townName))
            {
                throw new ArgumentException("Town name is required!", nameof(townName));
            }

            if (!this.context.Towns.Any(x => x.Name == townName))
            {
                throw new ArgumentException($"Town {townName} don't exists!");
            }
            var town = this.context.Towns.SingleOrDefault(x => x.Name == townName);

            if (this.context.Addresses.Any(x => x.AddressText == address && x.Town.Name == townName))
            {
                throw new ArgumentException($"Address {address} in town {townName} already exists!");
            }

            var addressToAdd = new Address()
            {
                AddressText = address,
                Town = town
            };

            this.context.Addresses.Add(addressToAdd);
            this.context.SaveChanges();
        }
    }
}
