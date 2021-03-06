﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using System.Collections.Generic;
using System;
using System.Linq;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.ProductModels.Contracts;

namespace OnlineStore.Logic.Services
{
    public class ProductService : IProductService
    {
        private readonly IOnlineStoreContext context;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public ProductService(IOnlineStoreContext context, IMapper mapper, ICategoryService categoryService)
        {
            this.context = context ?? throw new ArgumentNullException();
            this.mapper = mapper ?? throw new ArgumentNullException();
            this.categoryService = categoryService ?? throw new ArgumentNullException();
        }

        public IEnumerable<IProductModel> GetAllProducts()
        {
            return this.context.Products.ProjectTo<ProductModel>();
        }

        public IEnumerable<IProductModel> GetProductsByCategoryName(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentNullException();
            }
            var filteredProducts = this.context.Products.Where(w => w.Category.Name == categoryName);

            return filteredProducts.ProjectTo<ProductModel>();
        }

        public void AddProduct(IProductImportModel productModel)
        {
            if (productModel == null)
            {
                throw new ArgumentNullException(nameof(productModel));
            }

            if (this.ProductExistsByName(productModel.Name))
            {
                throw new ArgumentException($"Product {productModel.Name} already exists!");
            }

            var category = this.context.Categories.SingleOrDefault(x => x.Name == productModel.Category)
                ?? throw new ArgumentException("Category not found!");
            var supplier = this.context.Suppliers.SingleOrDefault(x => x.Name == productModel.Supplier)
                ?? throw new ArgumentException("Supplier not found!");

            var productToAdd = this.mapper.Map<Product>(productModel);
            productToAdd.Category = category;
            productToAdd.Supplier = supplier;

            this.context.Products.Add(productToAdd);
            this.context.SaveChanges();
        }

        public IProductModel FindProductByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Product name is required!");
            }

            var product = this.context.Products.FirstOrDefault(x => x.Name == name) ?? throw new ArgumentException("No such product!");

            var productModel = mapper.Map<IProductModel>(product);
            return productModel;
        }

        public void RemoveProductByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Product name is required!");
            }

            var productToRemove = this.context.Products.FirstOrDefault(x => x.Name == name) ?? throw new ArgumentException("No such product!");
            this.context.Products.Remove(productToRemove);
            this.context.SaveChanges();
        }

        public void AddProductRange(IEnumerable<IProductImportModel> productModels)
        {
            if (productModels == null)
            {
                throw new ArgumentNullException(nameof(productModels));
            }

            var productsToAdd = new List<Product>();

            foreach (var productModel in productModels)
            {
                var productToAdd = this.mapper.Map<IProductImportModel, Product>(productModel);

                if (!this.context.Categories.Any(x => x.Name == productModel.Category))
                {
                    this.categoryService.Create(productModel.Category);
                }
                var category = this.context.Categories.SingleOrDefault(x => x.Name == productModel.Category);
                var supplier = this.context.Suppliers.FirstOrDefault(x => x.Name == productModel.Supplier);
                productToAdd.Category = category;
                productToAdd.Supplier = supplier ?? throw new ArgumentException("Supplier not found!");

                productsToAdd.Add(productToAdd);
            }


            productsToAdd.ForEach(p => this.context.Products.Add(p));
            this.context.SaveChanges();
        }

        public bool ProductExistsByName(string productName)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException();
            }

            var productFound = this.context.Products
                .FirstOrDefault(w => w.Name == productName);

            return productFound != null;
        }
    }
}