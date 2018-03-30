using System;
using System.Collections.Generic;
using OnlineStore.Core.Contracts;
using OnlineStore.DTO.CourierModels;
using OnlineStore.DTO.ProductModels.Contracts;
using OnlineStore.DTO.SupplierModels;
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
        internal Func<string> ExposedImportSuppliersFunction => base.ImportSuppliers;
        internal Func<string> ExposedImportCouriersFunction => base.ImportCouriers;

        internal IList<IProductImportModel> ExposedValidProducts => new List<IProductImportModel>(base.ValidProducts);
        internal IList<ISuppliersImportModel> ExposedValidSuppliers => new List<ISuppliersImportModel>(base.ValidSuppliers);
        internal IList<ICourierImportModel> ExposedValidCouriers => new List<ICourierImportModel>(base.ValidCouriers);

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
