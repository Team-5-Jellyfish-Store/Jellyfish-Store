using System.Collections.Generic;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.SupplierModels;
using OnlineStore.DTO.ProductModels.Contracts;

namespace OnlineStore.Logic.Contracts
{
    public interface ISupplierService
    {
        IProductModel GetSupplierByName(string name);

        void AddSupplierRange(IList<ISuppliersImportModel> suppliers);

        bool SupplierExistsByName(string name);

    }
}
