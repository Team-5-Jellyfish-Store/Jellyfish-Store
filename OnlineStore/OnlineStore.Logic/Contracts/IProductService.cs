using OnlineStore.DTO.ProductModels;
using System.Collections.Generic;

namespace OnlineStore.Logic.Contracts
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetAllProducts();
        IEnumerable<ProductModel> GetProductsByCategoryName(string categoryName);

        ProductModel FindProductByName(string name);

        void RemoveProductByName(string name);

        void AddProduct(ProductImportModel product);

        void AddProductRange(IList<ProductImportModel> products);

        bool ProductExistsByName(string productName);
    }
}