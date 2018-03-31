using System.Collections.Generic;
using OnlineStore.DTO.SupplierModels;
using OnlineStore.DTO.ProductModels.Contracts;

namespace OnlineStore.Logic.Contracts
{
    public interface ISupplierService
    {
        void AddSupplierRange(IEnumerable<ISuppliersImportModel> suppliers);

        bool SupplierExistsByName(string name);

    }
}
