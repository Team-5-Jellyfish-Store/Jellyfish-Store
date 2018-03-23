using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using System.Collections.Generic;
using System;
using System.Linq;

namespace OnlineStore.Logic.Services
{
    public class ProductService : IProductService
    {
        private readonly IOnlineStoreContext context;
        private readonly ICategoryService categoryService;
        private readonly ISupplierService supplierService;
        private readonly IMapper mapper;

        public ProductService(IOnlineStoreContext context, IMapper mapper, ICategoryService categoryService, ISupplierService supplierService)
        {
            this.context = context;
            this.mapper = mapper;
            this.categoryService = categoryService;
            this.supplierService = supplierService;
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            return context.Products.ProjectTo<ProductModel>();
        }

        public void AddProduct(ProductImportModel product)
        {
            var productToAdd = this.mapper.Map<ProductImportModel, Product>(product);

            this.context.Products.Add(productToAdd);
            this.context.SaveChanges();
        }

        public void CreateProduct(string productName, decimal purchasePrice, int quantity, string categoryName,
            string supplierName)
        {
            var categoryId = this.categoryService.GetIdByName(categoryName);
            var supplierId = this.supplierService.GetIdByName(supplierName);

            var product = new ProductImportModel
            {
                Name = productName,
                PurchasePrice = purchasePrice,
                Quantity = quantity,
                CategoryId = categoryId,
                SupplierId = supplierId
            };

            this.AddProduct(product);
        }
        public ProductModel FindProductByName(string name)
        {
            var product = context.Products.FirstOrDefault(x => x.Name == name) ?? throw new ArgumentException("No such product!"); ;
          
            var productModel = mapper.Map<ProductModel>(product);
            return productModel;
        }

        public void RemoveProductByName(string name)
        {
            var productToRemove = context.Products.FirstOrDefault(x => x.Name == name) ?? throw new ArgumentException("No such product!");
            context.Products.Remove(productToRemove);
            context.SaveChanges();
        }
    }
}