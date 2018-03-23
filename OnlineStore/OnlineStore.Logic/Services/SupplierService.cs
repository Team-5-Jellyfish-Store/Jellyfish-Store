using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;

namespace OnlineStore.Logic.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IOnlineStoreContext context;
        private readonly IMapper mapper;

        public SupplierService(IOnlineStoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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

        public void AddSupplierRange(List<Supplier> suppliers)
        {
            suppliers.ForEach(s => this.context.Suppliers.Add(s));
            this.context.SaveChanges();
        }

        public Supplier FindByName(string name)
        {
            var foundSupplier = this.context.Suppliers.SingleOrDefault(x => x.Name == name) ?? throw new ArgumentNullException();


            return foundSupplier;
        }
    }
}
