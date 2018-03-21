using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Logic.Services
{
    public class ImportService : IImportService
    {
        private readonly ICourierService courierService;
        private readonly IProductService productService;
        private readonly ISupplierService supplierService;

        public ImportService(ICourierService courierService, IProductService productService, ISupplierService supplierService)
        {
            this.courierService = courierService;
            this.productService = productService;
            this.supplierService = supplierService;
        }


        public string Import()
        {
            var importResults = new StringBuilder();
            var couriersImportString = File.ReadAllText("../../../Datasets/Couriers.json");

            var deserializedCouriers = JsonConvert.DeserializeObject<CourierImportDto[]>(couriersImportString);

            foreach (var courierImportDto in deserializedCouriers)
            {
                
            }
            return importResults.ToString().Trim();
        }

        private bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj); // System.Components.Data.Annotations
            var validationResults = new List<ValidationResult>();

            var isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return isValid;
        }
    }
}
