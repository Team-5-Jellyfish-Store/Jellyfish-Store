using System.Collections.Generic;
using System.Text;
using OnlineStore.Logic.Contracts;
using OnlineStore.DTO.CourierModels;
using OnlineStore.DTO.SupplierModels;
using System;
using OnlineStore.Core.Contracts;
using OnlineStore.Providers.Contracts;
using OnlineStore.DTO.ProductModels.Contracts;

namespace OnlineStore.Logic.Services
{
    public class ImportService : IImportService
    {
        private const string failureMessage = "Import rejected. Input is with invalid format.";

        private readonly string productsJSONPathString = "../../../Datasets/Products.json";
        private readonly string suppliersJSONPathString = "../../../Datasets/Suppliers.json";
        private readonly string couriersJSONPathString = "../../../Datasets/Couriers.json";

        private Func<string> ImportSuppliersFunction => this.ImportSuppliers;
        private Func<string> ImportCouriersFunction => this.ImportCouriers;
        private Func<string> ImportProductsFunction => this.ImportProducts;

        private readonly ISupplierService supplierService;
        private readonly IFileReader fileReader;
        private readonly IValidator validator;
        private readonly IProductService productService;
        private readonly ICourierService courierService;
        private readonly IJsonService jsonService;

        public ImportService(IProductService productService, ICourierService courierService, ISupplierService supplierService, IFileReader fileReader, IValidator validator, IJsonService jsonService)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.courierService = courierService ?? throw new ArgumentNullException(nameof(courierService));
            this.supplierService = supplierService ?? throw new ArgumentNullException(nameof(supplierService));
            this.fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.jsonService = jsonService ?? throw new ArgumentNullException(nameof(validator));
        }

        protected IList<IProductImportModel> ValidProducts { get; private set; }
        protected IList<ISuppliersImportModel> ValidSuppliers { get; private set; }
        protected IList<ICourierImportModel> ValidCouriers { get; private set; }

        public string Import()
        {
            var importResults = new StringBuilder();

            var supplierImportResults = ImportSuppliersFunction();

            importResults.AppendLine(supplierImportResults);

            var courierImportResults = ImportCouriersFunction();

            importResults.AppendLine(courierImportResults);

            var productImportResults = ImportProductsFunction();

            importResults.AppendLine(productImportResults);

            return importResults.ToString().Trim();
        }

        protected virtual string ImportProducts()
        {
            var importProductsResults = new StringBuilder();

            var importProductString = this.fileReader.ReadAllText(this.productsJSONPathString);
            var deserializedProducts = this.jsonService.DeserializeProducts(importProductString);

            this.ValidProducts = new List<IProductImportModel>();

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

                this.ValidProducts.Add(productDto);
                
                importProductsResults.AppendLine($"{productDto.Quantity} items of product {productDto.Name} added successfully!");
            }

            this.productService.AddProductRange(ValidProducts);

            return importProductsResults.ToString();
        }

        protected virtual string ImportSuppliers()
        {
            var importSuppliersResults = new StringBuilder();

            var suppliersImportString = this.fileReader.ReadAllText(this.suppliersJSONPathString);
            var deserializedSuppliers = this.jsonService.DeserializeSuppliers(suppliersImportString);

            this.ValidSuppliers = new List<ISuppliersImportModel>();

          
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
                this.ValidSuppliers.Add(supplierDto);

                importSuppliersResults.AppendLine($"Supplier {supplierDto.Name} added successfully!");
            }

            this.supplierService.AddSupplierRange(this.ValidSuppliers);

            return importSuppliersResults.ToString();
        }

        protected virtual string ImportCouriers()
        {
            var importCourierResults = new StringBuilder();

            var importCouriersString = this.fileReader.ReadAllText(this.couriersJSONPathString);
            var deserializedCouriers = this.jsonService.DeserializeCouriers(importCouriersString);

            this.ValidCouriers = new List<ICourierImportModel>();

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

                this.ValidCouriers.Add(courierDto);
                importCourierResults.AppendLine($"Courier {courierDto.FirstName} {courierDto.LastName} added successfully!");
            }

            this.courierService.AddCourierRange(this.ValidCouriers);

            return importCourierResults.ToString();
        }
    }
}
