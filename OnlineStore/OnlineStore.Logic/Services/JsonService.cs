using System;
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
            if (string.IsNullOrEmpty(suppliersImportString))
            {
                throw new ArgumentNullException();
            }

            var deserializedSuppliers = JsonConvert.DeserializeObject<SuppliersImportModel[]>(suppliersImportString);
            return deserializedSuppliers;
        }

        public CourierImportModel[] DeserializeCouriers(string importCouriersString)
        {
            if (string.IsNullOrEmpty(importCouriersString))
            {
                throw new ArgumentNullException();
            }

            var deserializedCouriers = JsonConvert.DeserializeObject<CourierImportModel[]>(importCouriersString);
            return deserializedCouriers;
        }

        public ProductImportModel[] DeserializeProducts(string importProductString)
        {
            if (string.IsNullOrEmpty(importProductString))
            {
                throw new ArgumentNullException();
            }

            var deserializedProducts = JsonConvert.DeserializeObject<ProductImportModel[]>(importProductString);
            return deserializedProducts;
        }
    }
}
