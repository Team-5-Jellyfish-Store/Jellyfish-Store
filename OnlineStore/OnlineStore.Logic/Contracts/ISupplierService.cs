using System.Collections.Generic;
using OnlineStore.DTO;
using OnlineStore.Models.DataModels;

namespace OnlineStore.Logic.Contracts
{
    public interface ISupplierService
    {
        ProductModel GetSupplierByName(string name);

        int GetIdByName(string name);

        IEnumerable<ProductModel> GetAllProducts();

        void AddSupplierRange(List<Supplier> suppliers);

        Supplier FindByName(string name);
    }
}
