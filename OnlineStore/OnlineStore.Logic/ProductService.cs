using AutoMapper.QueryableExtensions;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using System.Collections.Generic;
using AutoMapper;

namespace OnlineStore.Logic
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
            var productToAdd = this.mapper.Map<ProductImportModel,Product>(product);

            this.context.Products.Add(productToAdd);
            this.context.SaveChanges();
        }

        public void CreateProduct(string productName, decimal purchasePrice, int quantity, string categoryName,
            string supplierName)
        {
            var categoryId = this.categoryService.FindIdByName(categoryName);
            var supplierId = this.supplierService.GetSupplierIdByName(supplierName);
            
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
    }
}