using System.Linq;
using OnlineStore.DTO;

namespace OnlineStore.Logic.Contracts
{
    public interface IProductService
    {
        IQueryable<ProductImportModel> GetAllPosts();

        void AddProduct(ProductImportModel product);
    }
}
