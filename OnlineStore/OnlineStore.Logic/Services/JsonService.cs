using Newtonsoft.Json;
using OnlineStore.DTO.CourierModels;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.SupplierModels;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Logic.Services
{
    public class JsonService : IJsonService
    {
        public SuppliersImportModel[] DeserializeSuppliers(string suppliersImportString)
        {
            var deserializedSuppliers = JsonConvert.DeserializeObject<SuppliersImportModel[]>(suppliersImportString);
            return deserializedSuppliers;
        }

        public CourierImportModel[] DeserializeCouriers(string importCouriersString)
        {
            var deserializedCouriers = JsonConvert.DeserializeObject<CourierImportModel[]>(importCouriersString);
            return deserializedCouriers;
        }

        public ProductImportModel[] DeserializeProducts(string importProductString)
        {
            var deserializedProducts = JsonConvert.DeserializeObject<ProductImportModel[]>(importProductString);
            return deserializedProducts;
        }
    }
}
