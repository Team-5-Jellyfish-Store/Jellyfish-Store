using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using OnlineStore.Logic.Contracts;
using OnlineStore.DTO.CourierModels;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.SupplierModels;
using System;
using OnlineStore.Core.Contracts;
using OnlineStore.Providers.Contracts;

namespace OnlineStore.Logic.Services
{
    public class ImportService : IImportService
    {
        private const string failureMessage = "Import rejected. Input is with invalid format.";

        private readonly string productsJSONPathString = "../../../Datasets/Products.json";
        private readonly string suppliersJSONPathString = "../../../Datasets/Suppliers.json";
        private readonly string couriersJSONPathString = "../../../Datasets/Couriers.json";

        private readonly ISupplierService supplierService;
        private readonly IFileReader fileReader;
        private readonly IValidator validator;
        private readonly IProductService productService;
        private readonly ICourierService courierService;

        public ImportService(IProductService productService, ICourierService courierService, ISupplierService supplierService, IFileReader fileReader, IValidator validator)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.courierService = courierService ?? throw new ArgumentNullException(nameof(courierService));
            this.supplierService = supplierService ?? throw new ArgumentNullException(nameof(supplierService));
            this.fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
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

        protected string ImportProducts()
        {
            var importProductsResults = new StringBuilder();

            var importString = this.fileReader.ReadAllText(this.productsJSONPathString);
            var deserializedProducts = JsonConvert.DeserializeObject<ProductImportModel[]>(importString);

            var validProducts = new List<ProductImportModel>();

            foreach (var productDto in deserializedProducts)
            {
                if (!this.validator.IsValid(productDto))
                {
                    importProductsResults.AppendLine(failureMessage);
                    continue;
                }

                var productExists = this.productService.ProductExistsByName(productDto.Name);

                if (productExists)
                {
                    importProductsResults.AppendLine($"Product {productDto.Name} already exists!");
                    continue;
                }

                validProducts.Add(productDto);
                importProductsResults.AppendLine($"{productDto.Quantity} items of product {productDto.Name} added successfully!");
            }

            this.productService.AddProductRange(validProducts);

            return importProductsResults.ToString();
        }

        protected string ImportSuppliers()
        {
            var importSuppliersResults = new StringBuilder();

            var suppliersImportString = this.fileReader.ReadAllText(this.suppliersJSONPathString);
            var deserializedSuppliers = JsonConvert.DeserializeObject<SuppliersImportModel[]>(suppliersImportString);

            var validSupplierModels = new List<SuppliersImportModel>();

            foreach (var supplierDto in deserializedSuppliers)
            {
                if (!this.validator.IsValid(supplierDto))
                {
                    importSuppliersResults.AppendLine(failureMessage);
                    continue;
                }

                var supplierExists = this.supplierService.SupplierExistsByName(supplierDto.Name);

                if (supplierExists)
                {
                    importSuppliersResults.AppendLine($"Supplier {supplierDto.Name} already exists!");
                    continue;
                }
                validSupplierModels.Add(supplierDto);

                importSuppliersResults.AppendLine($"Supplier {supplierDto.Name} added successfully!");
            }

            this.supplierService.AddSupplierRange(validSupplierModels);

            return importSuppliersResults.ToString();
        }

        protected string ImportCouriers()
        {
            var importCourierResults = new StringBuilder();

            var importCouriersResults = this.fileReader.ReadAllText(this.couriersJSONPathString);
            var deserializedCouriers = JsonConvert.DeserializeObject<CourierImportModel[]>(importCouriersResults);

            var validCouriers = new List<CourierImportModel>();

            foreach (var courierDto in deserializedCouriers)
            {
                if (!this.validator.IsValid(courierDto))
                {
                    importCourierResults.AppendLine(failureMessage);
                    continue;
                }

                var courierExists = this.courierService.CourierExistsByName(courierDto.FirstName, courierDto.LastName);

                if (courierExists)
                {
                    importCourierResults.AppendLine($"Courier {courierDto.FirstName} {courierDto.LastName} already exists!");
                    continue;
                }

                validCouriers.Add(courierDto);
                importCourierResults.AppendLine($"Courier {courierDto.FirstName} {courierDto.LastName} added successfully!");
            }

            this.courierService.AddCourierRange(validCouriers);

            return importCourierResults.ToString();
        }
    }
}
