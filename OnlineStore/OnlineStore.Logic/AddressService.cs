using AutoMapper;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using System;
using System.Linq;

namespace OnlineStore.Logic
{
    public class AddressService : IAddressService
    {
        private readonly IOnlineStoreContext context;
        private readonly IMapper mapper;

        public AddressService(IOnlineStoreContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public /*AddressModel*/ Address GetAddress(string addressText, string townName)
        {
            var town = this.context.Towns.SingleOrDefault(x => x.Name == townName);

            return town.Addresses.FirstOrDefault(x => x.AddressText == addressText);

            //return this.mapper.Map<AddressModel>(address);
        }
    }
}
