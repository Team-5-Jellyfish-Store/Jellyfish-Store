using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using Newtonsoft.Json;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace OnlineStore.Logic.Services
{
    public class ImportService : IImportService
    {
        private readonly ICourierService courierService;
        private readonly IProductService productService;
        private readonly ISupplierService supplierService;
        private readonly IOnlineStoreContext context;
        private readonly IMapper mapper;


        public ImportService(ICourierService courierService, IProductService productService, ISupplierService supplierService, IOnlineStoreContext context, IMapper mapper)
        {
            this.courierService = courierService;
            this.productService = productService;
            this.supplierService = supplierService;
            this.context = context;
            this.mapper = mapper;
        }


        public string Import()
        {
            string FailureMessage = "Import rejected. Input is with invalid format.";
            var importResults = new StringBuilder();
            var suppliersImportString = File.ReadAllText("../../../Datasets/Suppliers.json");

            var deserializedSuppliers = JsonConvert.DeserializeObject<SuppliersImportDto[]>(suppliersImportString);
            var validSuppliers = new List<Supplier>();
            foreach (var supplierDto in deserializedSuppliers)
            {
                if (!this.IsValid(supplierDto))
                {
                    importResults.AppendLine(FailureMessage);
                    continue;
                }

                var supplierToAdd = this.mapper.Map<SuppliersImportDto, Supplier>(supplierDto);
                
                supplierToAdd.Address.AddressText = supplierDto.Address;
                supplierToAdd.Address.Town.Name = supplierDto.Town;

                validSuppliers.Add(supplierToAdd);
                importResults.AppendLine($"Supplier {supplierDto.Name} added successfully!");
            }

            validSuppliers.ForEach(s => this.context.Suppliers.Add(s));
            this.context.SaveChanges();

            return importResults.ToString().Trim();
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
