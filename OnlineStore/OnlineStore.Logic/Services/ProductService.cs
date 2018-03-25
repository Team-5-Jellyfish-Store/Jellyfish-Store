using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using System.Collections.Generic;
using System;
using System.Linq;
using OnlineStore.DTO.ProductModels;

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
            this.context = context ?? throw new ArgumentNullException();
            this.mapper = mapper ?? throw new ArgumentNullException();
            this.categoryService = categoryService ?? throw new ArgumentNullException();
            this.supplierService = supplierService ?? throw new ArgumentNullException();
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            return context.Products.ProjectTo<ProductModel>();
        }

        public void AddProduct(ProductImportModel productModel)
        {
            if (productModel == null)
            {
                throw new ArgumentNullException(nameof(productModel));
            }

            if (this.context.Products.Any(x => x.Name == productModel.Name))
            {
                throw new ArgumentException($"Product {productModel.Name} already exists!");
            }

            var category = this.context.Categories.SingleOrDefault(x => x.Name == productModel.Category)
                ?? throw new ArgumentException("Category not found!");
            var supplier = this.context.Suppliers.SingleOrDefault(x => x.Name == productModel.Supplier)
                ?? throw new ArgumentException("Supplier not found!");

            var productToAdd = this.mapper.Map<ProductImportModel, Product>(productModel);
            productToAdd.Category = category;
            productToAdd.Supplier = supplier;

            this.context.Products.Add(productToAdd);
            this.context.SaveChanges();
        }

        public ProductModel FindProductByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Product name is required!", nameof(name));
            }

            var product = context.Products.FirstOrDefault(x => x.Name == name) ?? throw new ArgumentException("No such product!"); ;

            var productModel = mapper.Map<ProductModel>(product);
            return productModel;
        }

        public void RemoveProductByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Product name is required!", nameof(name));
            }

            var productToRemove = context.Products.FirstOrDefault(x => x.Name == name) ?? throw new ArgumentException("No such product!");
            context.Products.Remove(productToRemove);
            context.SaveChanges();
        }

        public void AddProductRange(List<ProductImportModel> productModels)
        {
            if (productModels == null)
            {
                throw new ArgumentNullException(nameof(productModels));
            }

            var products = new List<Product>();

            foreach (var productModel in productModels)
            {
                var productToAdd = this.mapper.Map<ProductImportModel, Product>(productModel);

                if (!this.context.Categories.Any(x => x.Name == productModel.Category))
                {
                    this.categoryService.Create(productModel.Category);
                }
                var category = this.context.Categories.SingleOrDefault(x => x.Name == productModel.Category);

                if (!this.context.Suppliers.Any(x => x.Name == productModel.Supplier))
                {
                    this.supplierService.Create(productModel.Supplier);
                }
                var supplier = this.context.Suppliers.SingleOrDefault(x => x.Name == productModel.Supplier);

                productToAdd.Category = category;
                productToAdd.Supplier = supplier;

                products.Add(productToAdd);
            }

            var newProducts = products
                .FindAll(x => this.context.Products
                                .All(y => y.Name != x.Name));

            newProducts.ForEach(p => this.context.Products.Add(p));
            this.context.SaveChanges();
        }
    }
}