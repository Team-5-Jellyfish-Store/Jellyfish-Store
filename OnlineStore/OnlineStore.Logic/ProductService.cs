using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;

namespace OnlineStore.Logic
{
    public class ProductService : IProductService
    {
        private readonly IOnlineStoreContext dbContext;
        private readonly IMapper mapper;

        public ProductService(IOnlineStoreContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void AddProduct(ProductImportModel product)
        {
            if (product == null)
            {
                throw new ArgumentException();
            }
            if (this.dbContext.Products.Any(x => x.Name.Contains(product.Name)))
            {
                Console.WriteLine("Product already exists!");
            }
            var productToAdd = this.mapper.Map<Product>(product);

            this.dbContext.Products.Add(productToAdd);
            this.dbContext.SaveChanges();
        }

        public IQueryable<ProductImportModel> GetAllPosts()
        {
            return this.dbContext.Products.ProjectTo<ProductImportModel>();
        }
    }
}
