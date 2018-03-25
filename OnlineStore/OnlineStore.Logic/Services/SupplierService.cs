using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.SupplierModels;

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

        public ProductModel GetSupplierByName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException();
            }

            var supplierFound = this.context.Suppliers
                .Where(w => w.Name == name)
                .ProjectTo<ProductModel>()
                .FirstOrDefault();

            return supplierFound ?? throw new ArgumentNullException();
        }

        public int GetIdByName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException();
            }

            var supplierFound = this.context
                .Suppliers
                .FirstOrDefault(w => w.Name == name) ?? throw new ArgumentException("Supplier not found!");

            return supplierFound.Id;
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            return context.Products.ProjectTo<ProductModel>();
        }

        public void AddSupplierRange(List<SuppliersImportModel> supplierModels)
        {
            if (supplierModels == null)
            {
                throw new ArgumentNullException(nameof(supplierModels));
            }

            var suppliers = new List<Supplier>();

            foreach (var supplierModel in supplierModels)
            {
                var supplierToAdd = this.mapper.Map<SuppliersImportModel, Supplier>(supplierModel);

                if (!this.context.Towns.Any(x => x.Name == supplierModel.Town))
                {
                    this.townService.Create(supplierModel.Town);
                }
                var supplierTown = this.context.Towns.SingleOrDefault(x => x.Name == supplierModel.Town);

                if (!this.context.Addresses.Any(x => x.AddressText == supplierModel.Address && x.Town.Name == supplierModel.Town))
                {
                    this.addressService.Create(supplierModel.Address, supplierTown.Name);
                }
                var supplierAddress = this.context.Addresses.FirstOrDefault(x => x.AddressText == supplierModel.Address && x.Town.Name == supplierModel.Town);

                supplierToAdd.Address = supplierAddress;

                suppliers.Add(supplierToAdd);
            }

            var newSuppliers = suppliers
                .FindAll(x => this.context.Suppliers
                                .All(y => y.Name != x.Name));

            newSuppliers.ForEach(s => this.context.Suppliers.Add(s));
            this.context.SaveChanges();
        }

        public void Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Supplier name is required!", nameof(name));
            }

            if (this.context.Suppliers.Any(x => x.Name == name))
            {
                throw new ArgumentException($"Supplier {name} already exists!");
            }

            var address = this.context.Addresses.FirstOrDefault();

            var supplierToAdd = new Supplier
            {
                //Name = name,
                //Phone = 
                //Address = 


            };

            this.context.Suppliers.Add(supplierToAdd);
            this.context.SaveChanges();
        }
    }
}
