using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using AutoMapper;
using Newtonsoft.Json;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO.ExternalImportDto;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace OnlineStore.Logic.Services
{
    public class ImportService : IImportService
    {
        const string failureMessage = "Import rejected. Input is with invalid format.";

        private readonly ICourierService courierService;
        private readonly IProductService productService;
        private readonly IAddressService addressService;
        private readonly ICategoryService categoryService;
        private readonly ISupplierService supplierService;
        private readonly IOnlineStoreContext context;
        private readonly IMapper mapper;


        public ImportService(ICourierService courierService, IProductService productService, ISupplierService supplierService, ICategoryService categoryService, IAddressService addressService, IOnlineStoreContext context, IMapper mapper)
        {
            this.addressService = addressService;
            this.courierService = courierService;
            this.productService = productService;
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

            //var productImportResults = ImportProducts();

            //importResults.AppendLine(productImportResults);

            return importResults.ToString().Trim();
        }

        private string ImportProducts()
        {
            var importProductsResults = new StringBuilder();

            var importString = File.ReadAllText("../../../Datasets/Products.json");
            var deserializedProducts = JsonConvert.DeserializeObject<ProductImportDto[]>(importString);

            var validProducts = new List<Product>();

            foreach (var productDto in deserializedProducts)
            {
                if (!this.IsValid(productDto))
                {
                    importProductsResults.AppendLine(failureMessage);
                    continue;
                }

                var productToAdd = this.mapper.Map<ProductImportDto, Product>(productDto);

                var categoryIdFound = this.categoryService.GetIdByName(productDto.Category);

                if (categoryIdFound == 0)
                {
                    productToAdd.Category.Name = productDto.Category;
                }
                else
                {
                    productToAdd.CategoryId = categoryIdFound;
                }
                var supplierIdFound = this.supplierService.GetIdByName(productDto.Supplier);

                if (supplierIdFound == 0)
                {
                    productToAdd.Supplier.Name = productDto.Supplier;
                }
                else
                {
                    productToAdd.SupplierId = supplierIdFound;
                }

                validProducts.Add(productToAdd);
                importProductsResults.AppendLine($"{productDto.Quantity} items of product {productDto.Name} added successfully!");
            }

            validProducts.ForEach(c => this.context.Products.Add(c));

            this.context.SaveChanges();
            var result = importProductsResults.ToString().Trim();
            return result;
        }

        private string ImportSuppliers()
        {
            var importSuppliersResults = new StringBuilder();

            var suppliersImportString = File.ReadAllText("../../../Datasets/Suppliers.json");

            var deserializedSuppliers = JsonConvert.DeserializeObject<SuppliersImportDto[]>(suppliersImportString);
            var validSuppliers = new List<Supplier>();

            foreach (var supplierDto in deserializedSuppliers)
            {
                if (!this.IsValid(supplierDto))
                {
                    importSuppliersResults.AppendLine(failureMessage);
                    continue;
                }

                var supplierToAdd = this.mapper.Map<SuppliersImportDto, Supplier>(supplierDto);

                var supplierAddress = this.addressService.FindOrCreate(supplierDto.Address, supplierDto.Town);

                supplierToAdd.Address.Id = supplierAddress.Id;

                validSuppliers.Add(supplierToAdd);
                importSuppliersResults.AppendLine($"Supplier {supplierDto.Name} added successfully!");
            }

            this.supplierService.AddSupplierRange(validSuppliers);

            return importSuppliersResults.ToString();
        }

        private string ImportCouriers()
        {
            var importCourierResults = new StringBuilder();

            var importCouriersResults = File.ReadAllText("../../../Datasets/Couriers.json");

            var deserializedCouriers = JsonConvert.DeserializeObject<CourierImportDto[]>(importCouriersResults);

            var validCouriers = new List<Courier>();

            foreach (var courierDto in deserializedCouriers)
            {
                if (!this.IsValid(courierDto))
                {
                    importCourierResults.AppendLine(failureMessage);
                    continue;
                }

                var courierToAdd = this.mapper.Map<CourierImportDto, Courier>(courierDto);

                var courierAddress = this.addressService.FindOrCreate(courierDto.Address, courierDto.Town);

                courierToAdd.Address.Id = courierAddress.Id;

                validCouriers.Add(courierToAdd);
                importCourierResults.AppendLine($"Courier {courierDto.FirstName} {courierDto.LastName} added successfully!");
            }
            this.courierService.AddCourierRange(validCouriers);
            var result = importCourierResults.ToString().Trim();
            return result;
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
