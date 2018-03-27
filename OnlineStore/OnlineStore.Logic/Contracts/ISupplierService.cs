using System.Collections.Generic;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.SupplierModels;

namespace OnlineStore.Logic.Contracts
{
    public interface ISupplierService
    {
        ProductModel GetSupplierByName(string name);

        void AddSupplierRange(IList<SuppliersImportModel> suppliers);

        bool SupplierExistsByName(string name);

    }
}
