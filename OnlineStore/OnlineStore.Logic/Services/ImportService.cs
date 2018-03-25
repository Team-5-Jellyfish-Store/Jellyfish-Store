using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using AutoMapper;
using Newtonsoft.Json;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
using System.Linq;
using OnlineStore.DTO.CourierModels;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.SupplierModels;

namespace OnlineStore.Logic.Services
{
    public class ImportService : IImportService
    {
        const string failureMessage = "Import rejected. Input is with invalid format.";

        private readonly ICategoryService categoryService;
        private readonly ISupplierService supplierService;
        private readonly IProductService productService;
        private readonly ICourierService courierService;
        private readonly IAddressService addressService;
        private readonly ITownService townService;
        private readonly IOnlineStoreContext context;
        private readonly IMapper mapper;


        public ImportService(IProductService productService, ICourierService courierService, ISupplierService supplierService, ICategoryService categoryService, IAddressService addressService, ITownService townService, IOnlineStoreContext context, IMapper mapper)
        {
            this.addressService = addressService;
            this.townService = townService ?? throw new System.ArgumentNullException(nameof(townService));
            this.productService = productService ?? throw new System.ArgumentNullException(nameof(productService));
            this.courierService = courierService;
            this.supplierService = supplierService;
            this.categoryService = categoryService;
            this.context = context;
            this.mapper = mapper;
        }

        public string Import()
        {
            var importResults = new StringBuilder();

            var supplierImportResults = ImportSuppliers();

            importResults.AppendLine(supplierImportResults);

            var courierImportResults = ImportCouriers();

            importResults.AppendLine(courierImportResults);

            var productImportResults = ImportProducts();

            importResults.AppendLine(productImportResults);

            return importResults.ToString().Trim();
        }

        private string ImportProducts()
        {
            var importProductsResults = new StringBuilder();

            var importString = File.ReadAllText("../../../Datasets/Products.json");
            var deserializedProducts = JsonConvert.DeserializeObject<ProductImportModel[]>(importString);

            var validProducts = new List<ProductImportModel>();

            foreach (var productDto in deserializedProducts)
            {
                if (!this.IsValid(productDto))
                {
                    importProductsResults.AppendLine(failureMessage);
                    continue;
                }

                validProducts.Add(productDto);
                importProductsResults.AppendLine($"{productDto.Quantity} items of product {productDto.Name} added successfully!");
            }

            this.productService.AddProductRange(validProducts);

            return importProductsResults.ToString().Trim();
        }

        private string ImportSuppliers()
        {
            var importSuppliersResults = new StringBuilder();

            var suppliersImportString = File.ReadAllText("../../../Datasets/Suppliers.json");
            var deserializedSuppliers = JsonConvert.DeserializeObject<SuppliersImportModel[]>(suppliersImportString);

            var validSupplierModels = new List<SuppliersImportModel>();

            foreach (var supplierDto in deserializedSuppliers)
            {
                if (!this.IsValid(supplierDto))
                {
                    importSuppliersResults.AppendLine(failureMessage);
                    continue;
                }

                validSupplierModels.Add(supplierDto);

                importSuppliersResults.AppendLine($"Supplier {supplierDto.Name} added successfully!");
            }

            this.supplierService.AddSupplierRange(validSupplierModels);

            return importSuppliersResults.ToString();
        }

        private string ImportCouriers()
        {
            var importCourierResults = new StringBuilder();

            var importCouriersResults = File.ReadAllText("../../../Datasets/Couriers.json");
            var deserializedCouriers = JsonConvert.DeserializeObject<CourierImportModel[]>(importCouriersResults);

            var validCouriers = new List<CourierImportModel>();

            foreach (var courierDto in deserializedCouriers)
            {
                if (!this.IsValid(courierDto))
                {
                    importCourierResults.AppendLine(failureMessage);
                    continue;
                }

                validCouriers.Add(courierDto);
                importCourierResults.AppendLine($"Courier {courierDto.FirstName} {courierDto.LastName} added successfully!");
            }

            this.courierService.AddCourierRange(validCouriers);

            return importCourierResults.ToString().Trim();
        }

        private bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return isValid;
        }
    }
}
