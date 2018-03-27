﻿using System.Linq;
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

        public void AddCourierRange(IList<CourierImportModel> courierModels)
        {
            if (courierModels == null)
            {
                throw new ArgumentNullException(nameof(courierModels));
            }

            var couriersToAdd = new List<Courier>();

            foreach (var courierModel in courierModels)
            {
                var courierToAdd = this.mapper.Map<CourierImportModel, Courier>(courierModel);

                if (!this.context.Towns.Any(x => x.Name == courierModel.TownName))
                {
                    this.townService.Create(courierModel.TownName);
                }
                var supplierTown = this.context.Towns.SingleOrDefault(x => x.Name == courierModel.TownName);

                if (!this.context.Addresses.Any(x => x.AddressText == courierModel.AddressText && x.Town.Name == courierModel.TownName))
                {
                    this.addressService.Create(courierModel.AddressText, supplierTown.Name);
                }
                var courierAddress = this.context.Addresses.FirstOrDefault(x => x.AddressText == courierModel.AddressText && x.Town.Name == courierModel.TownName);

                courierToAdd.Address = courierAddress;

                couriersToAdd.Add(courierToAdd);
            }

            couriersToAdd.ForEach(c => this.context.Couriers.Add(c));
            this.context.SaveChanges();
        }

        public bool CourierExistsByName(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentNullException();
            }

            var courierFound = this.context.Couriers
                .FirstOrDefault(w => w.FirstName == firstName && w.LastName == lastName);

            return courierFound != null;
        }
    }
}