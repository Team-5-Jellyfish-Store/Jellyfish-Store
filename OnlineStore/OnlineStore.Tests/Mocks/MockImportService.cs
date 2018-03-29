using System;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Logic.Services;
using OnlineStore.Providers.Contracts;

namespace OnlineStore.Tests.Mocks
{
    public class MockImportService : ImportService
    {

        public MockImportService(IProductService productService, ICourierService courierService, ISupplierService supplierService, IFileReader fileReader, IValidator validator, IJsonService jsonService) : base(productService, courierService, supplierService, fileReader, validator, jsonService)
        {
        }

        internal Func<string> ExposedImportProductsFunction => base.ImportProducts;

        protected override string ImportCouriers()
        {
            return "Courier method invoked!";
        }

        protected override string ImportProducts()
        {
            return "Products Method Invoked!";
        }

        protected override string ImportSuppliers()
        {
            return "Suppliers method invoked!";
        }

       
    }
}
