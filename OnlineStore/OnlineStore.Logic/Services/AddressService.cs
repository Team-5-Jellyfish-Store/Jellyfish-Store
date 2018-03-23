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
        private readonly ITownService townService;
        private readonly IMapper mapper;

        public AddressService(IOnlineStoreContext context, IMapper mapper, ITownService townService)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
        }

        public /*AddressModel*/ Address GetAddress(string addressText, string townName)
        {
            var town = this.context.Towns.SingleOrDefault(x => x.Name == townName);

            return town.Addresses.FirstOrDefault(x => x.AddressText == addressText);

            //return this.mapper.Map<AddressModel>(address);
        }

        public Address FindOrCreate(string address, string town)
        {
            var foundAddress = this.context.Addresses.SingleOrDefault(x => x.AddressText == address && x.Town.Name == town);
            

            if (foundAddress == null)
            {
                var foundTown = this.townService.FindOrCreate(town);
                foundAddress = this.Create(address, foundTown);
            }

            return foundAddress;
        }

        public Address Create(string address, Town town)
        {
            var addressToAdd = new Address()
            {
                AddressText = address,
                Town = town
            };
            this.context.Addresses.Add(addressToAdd);
            this.context.SaveChanges();

            return this.context.Addresses.First(f => f.AddressText == address);

        }
    }
}
