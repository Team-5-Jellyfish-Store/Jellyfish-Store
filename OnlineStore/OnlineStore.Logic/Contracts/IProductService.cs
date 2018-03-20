using OnlineStore.DTO;
using System.Collections.Generic;

namespace OnlineStore.Logic.Contracts
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetAllProducts();

        ProductModel FindProductByName(string name);

        void RemoveProductByName(string name);
        void AddProduct(ProductImportModel product);

        void CreateProduct(string productName, decimal purchasePrice, int quantity, string categoryName,
            string supplierName);
    }
}