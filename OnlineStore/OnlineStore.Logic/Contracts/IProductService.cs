using OnlineStore.DTO;
using OnlineStore.DTO.ProductModels;
using OnlineStore.Models.DataModels;
using System.Collections.Generic;

namespace OnlineStore.Logic.Contracts
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetAllProducts();

        ProductModel FindProductByName(string name);

        void RemoveProductByName(string name);
        
        void AddProduct(ProductImportModel product);

        void AddProductRange(List<ProductImportModel> products);
    }
}