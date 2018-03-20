using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Logic
{
    public class ProductService : IProductService
    {
        private readonly IOnlineStoreContext context;
        private readonly IMapper mapper;

        public ProductService(IOnlineStoreContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public IEnumerable<ProductModel> GetAllProducts()
        {
            return context.Products.ProjectTo<ProductModel>();
        }
        public ProductModel FindProductByName(string name)
        {
            var product = context.Products.FirstOrDefault(x => x.Name == name);
            if (product==null)
            {
                throw new ArgumentNullException("No such product!");
            
            }
            var productModel = mapper.Map<ProductModel>(product);
            return productModel;
        }

        public void RemoveProductByName(string name)
        {
            var productToRemove = context.Products.FirstOrDefault(x => x.Name == name);
            if (productToRemove == null)
            {
                throw new ArgumentNullException("No such product!");
            }
            context.Products.Remove(productToRemove);
        }
    }
}