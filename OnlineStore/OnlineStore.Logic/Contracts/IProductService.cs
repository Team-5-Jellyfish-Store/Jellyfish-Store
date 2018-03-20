using OnlineStore.DTO;
using System.Collections.Generic;

namespace OnlineStore.Logic.Contracts
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetAllProducts();
        void AddProduct(ProductImportModel product);

        void CreateProduct(string productName, decimal purchasePrice, int quantity, string categoryName,
            string supplierName);
    }
}