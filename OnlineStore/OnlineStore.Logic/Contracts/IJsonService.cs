using OnlineStore.DTO.CourierModels;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.SupplierModels;

namespace OnlineStore.Logic.Contracts
{
    public interface IJsonService
    {
        SuppliersImportModel[] DeserializeSuppliers(string suppliersImportString);

        CourierImportModel[] DeserializeCouriers(string importCouriersString);

        ProductImportModel[] DeserializeProducts(string importProductString);
    }
}
