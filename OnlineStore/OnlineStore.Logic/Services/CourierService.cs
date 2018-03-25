using System.Linq;
using System.Collections.Generic;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using System;
using AutoMapper;
using OnlineStore.DTO.CourierModels;

namespace OnlineStore.Logic.Services
{
    public class CourierService : ICourierService
    {
        private readonly IOnlineStoreContext context;
        private readonly ITownService townService;
        private readonly IAddressService addressService;
        private readonly IMapper mapper;

        public CourierService(IOnlineStoreContext context, ITownService townService, IAddressService addressService, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
            this.addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void AddCourierRange(List<CourierImportModel> courierModels)
        {
            if (courierModels == null)
            {
                throw new ArgumentNullException(nameof(courierModels));
            }

            var couriers = new List<Courier>();

            foreach (var courielModel in courierModels)
            {
                var courierToAdd = this.mapper.Map<CourierImportModel, Courier>(courielModel);

                if (!this.context.Towns.Any(x => x.Name == courielModel.Town))
                {
                    this.townService.Create(courielModel.Town);
                }
                var supplierTown = this.context.Towns.SingleOrDefault(x => x.Name == courielModel.Town);

                if (!this.context.Addresses.Any(x => x.AddressText == courielModel.Address && x.Town.Name == courielModel.Town))
                {
                    this.addressService.Create(courielModel.Address, supplierTown.Name);
                }
                var courierAddress = this.context.Addresses.FirstOrDefault(x => x.AddressText == courielModel.Address && x.Town.Name == courielModel.Town);

                courierToAdd.Address = courierAddress;

                couriers.Add(courierToAdd);
            }

            var newCouriers = couriers
                .FindAll(x => this.context.Couriers
                                .All(y => y.FirstName != x.FirstName
                                            ||
                                            y.LastName != x.LastName));

            newCouriers.ForEach(c => this.context.Couriers.Add(c));
            this.context.SaveChanges();
        }
    }
}