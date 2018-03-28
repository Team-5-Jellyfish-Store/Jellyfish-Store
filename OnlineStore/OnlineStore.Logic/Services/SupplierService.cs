using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.SupplierModels;
using OnlineStore.DTO.ProductModels.Contracts;

namespace OnlineStore.Logic.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IOnlineStoreContext context;
        private readonly IAddressService addressService;
        private readonly ITownService townService;
        private readonly IMapper mapper;

        public SupplierService(IOnlineStoreContext context, IAddressService addressService, ITownService townService, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IProductModel GetSupplierByName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException();
            }

            var supplierFound = this.context.Suppliers
                .Where(w => w.Name == name)
                .ProjectTo<ProductModel>()
                .FirstOrDefault();

            return supplierFound ?? throw new ArgumentException("Supplier with that name not found");
        }

        public bool SupplierExistsByName(string name)
        {

            if (name == null)
            {
                throw new ArgumentNullException();
            }

            var supplierFound = this.context.Suppliers
                .FirstOrDefault(w => w.Name == name);

            return supplierFound != null;
        }

        public void AddSupplierRange(IList<ISuppliersImportModel> supplierModels)
        {
            if (supplierModels == null)
            {
                throw new ArgumentNullException(nameof(supplierModels));
            }

            var suppliersToAdd = new List<Supplier>();

            foreach (var supplierModel in supplierModels)
            {
                var supplierToAdd = this.mapper.Map<ISuppliersImportModel, Supplier>(supplierModel);

                if (!this.context.Towns.Any(x => x.Name == supplierModel.TownName))
                {
                    this.townService.Create(supplierModel.TownName);
                }
                var supplierTown = this.context.Towns.SingleOrDefault(x => x.Name == supplierModel.TownName);

                if (!this.context.Addresses.Any(x => x.AddressText == supplierModel.AddressText && x.Town.Name == supplierModel.TownName))
                {
                    this.addressService.Create(supplierModel.AddressText, supplierTown.Name);
                }
                var supplierAddress = this.context.Addresses.FirstOrDefault(x => x.AddressText == supplierModel.AddressText && x.Town.Name == supplierModel.TownName);

                supplierToAdd.Address = supplierAddress;

                suppliersToAdd.Add(supplierToAdd);
            }
            
            suppliersToAdd.ForEach(s => this.context.Suppliers.Add(s));
            this.context.SaveChanges();
        }
        
    }
}
