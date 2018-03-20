using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Logic.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IOnlineStoreContext context;

        public SupplierService(IOnlineStoreContext context)
        {
            this.context = context;
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

        public int GetSupplierIdByName(string name)
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
    }
}
