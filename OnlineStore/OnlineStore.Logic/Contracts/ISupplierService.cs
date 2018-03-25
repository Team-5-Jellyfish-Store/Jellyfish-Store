using System.Collections.Generic;
using OnlineStore.DTO;
using OnlineStore.Models.DataModels;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.SupplierModels;

namespace OnlineStore.Logic.Contracts
{
    public interface ISupplierService
    {
        ProductModel GetSupplierByName(string name);

        int GetIdByName(string name);

        IEnumerable<ProductModel> GetAllProducts();

        void AddSupplierRange(List<SuppliersImportModel> suppliers);

        void Create(string supplier);
    }
}
