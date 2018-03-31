using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Common.AutoMapperConfig;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.ProductModels.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using OnlineStore.Tests.Mocks;

namespace OnlineStore.Tests.Services.ProductService
{
    [TestClass]
    public class AddProductRange_Should
    {
        [TestMethod]
        public void ThrowNullArgumentException_WhenProductModelsIsNull()
        {
            //Arrange
            var fakeContext = new Mock<IOnlineStoreContext>();
            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();
            var service = new Logic.Services.ProductService(fakeContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => service.AddProductRange(null));
        }

        [TestMethod]
        public void InvokeCategoryServiceCreate_WhenCategoryNotAvailable()
        {
            //Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var productToImport = new ProductImportModel
            {
                Category = "Patkiii",
                Name = "TestProduct",
                PurchasePrice = 50m,
                Quantity = 5,
                Supplier = "TestSupplier"
            };
            var productsToImport = new List<ProductImportModel> { productToImport };

            var mockContext = new Mock<IOnlineStoreContext>();
            var productsMock = new List<Product>().GetQueryableMockDbSet();
            var categoriesMock = new List<Category> { new Category { Name = "TestCategory" } }.GetQueryableMockDbSet();
            var categoriesAddedMock = new List<Category> { new Category { Name = "Patkiii" } }.GetQueryableMockDbSet();
            var suppliersMock = new List<Supplier> { new Supplier { Name = "TestSupplier" } }.GetQueryableMockDbSet();
            var fakemapper = new Mock<IMapper>();
            fakemapper.Setup(x => x.Map<IProductImportModel, Product>(It.IsAny<IProductImportModel>())).Returns(new Product { Name = productToImport.Name, PurchasePrice = productToImport.PurchasePrice, SellingPrice = productToImport.PurchasePrice * 1.5m, Quantity = productToImport.Quantity });

            var fakeCategoryService = new Mock<ICategoryService>();
            mockContext.SetupSequence(s => s.Categories).Returns(categoriesMock.Object).Returns(categoriesAddedMock.Object);
            mockContext.Setup(s => s.Suppliers).Returns(suppliersMock.Object);
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            var service = new Logic.Services.ProductService(mockContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act
            service.AddProductRange(productsToImport);

            //Assert
            fakeCategoryService.Verify(v => v.Create(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ThrowArgumentException_WhenSupplierNotFound()
        {
            //Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var productToImport = new ProductImportModel
            {
                Category = "TestCategory",
                Name = "TestProduct",
                PurchasePrice = 50m,
                Quantity = 5,
                Supplier = "Pesho"
            };
            var productsToImport = new List<ProductImportModel> { productToImport };

            var mockContext = new Mock<IOnlineStoreContext>();
            var productsMock = new List<Product>().GetQueryableMockDbSet();
            var categoriesMock = new List<Category> { new Category { Name = "TestCategory" } }.GetQueryableMockDbSet();
            var suppliersMock = new List<Supplier> { new Supplier { Name = "TestSupplier" } }.GetQueryableMockDbSet();
            var fakemapper = new Mock<IMapper>();
            fakemapper.Setup(x => x.Map<IProductImportModel, Product>(It.IsAny<IProductImportModel>())).Returns(new Product { Name = productToImport.Name, PurchasePrice = productToImport.PurchasePrice, SellingPrice = productToImport.PurchasePrice * 1.5m, Quantity = productToImport.Quantity });

            var fakeCategoryService = new Mock<ICategoryService>();
            mockContext.Setup(s => s.Categories).Returns(categoriesMock.Object);
            mockContext.Setup(s => s.Suppliers).Returns(suppliersMock.Object);
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            var service = new Logic.Services.ProductService(mockContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act && Assert
            Assert.ThrowsException<ArgumentException>(() => service.AddProductRange(productsToImport));
        }

        [TestMethod]
        public void InvokeAddMethod_WhenModelsAreValid()
        {
            //Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var productToImport = new ProductImportModel
            {
                Category = "TestCategory",
                Name = "TestProduct 2",
                PurchasePrice = 50m,
                Quantity = 5,
                Supplier = "TestSupplier"
            };
            var productToImport2 = new ProductImportModel
            {
                Category = "TestCategory",
                Name = "TestProduct 5",
                PurchasePrice = 50m,
                Quantity = 5,
                Supplier = "TestSupplier"
            };
            var productsToImport = new List<ProductImportModel> { productToImport, productToImport2 };
            var mockContext = new Mock<IOnlineStoreContext>();
            var productsMock = new List<Product> ().GetQueryableMockDbSet();
            var categoriesMock = new List<Category> { new Category { Name = "TestCategory" } }.GetQueryableMockDbSet();
            var suppliersMock = new List<Supplier> { new Supplier { Name = "TestSupplier" } }.GetQueryableMockDbSet();
            var fakemapper = new Mock<IMapper>();
            fakemapper.SetupSequence(x => x.Map<IProductImportModel, Product>(It.IsAny<IProductImportModel>())).Returns(new Product { Name = productToImport.Name, PurchasePrice = productToImport.PurchasePrice, SellingPrice = productToImport.PurchasePrice * 1.5m, Quantity = productToImport.Quantity }).Returns(new Product { Name = productToImport2.Name, PurchasePrice = productToImport2.PurchasePrice, SellingPrice = productToImport2.PurchasePrice * 1.5m, Quantity = productToImport2.Quantity });

            var fakeCategoryService = new Mock<ICategoryService>();
            mockContext.Setup(s => s.Categories).Returns(categoriesMock.Object);
            mockContext.Setup(s => s.Suppliers).Returns(suppliersMock.Object);
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            var service = new Logic.Services.ProductService(mockContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act
            service.AddProductRange(productsToImport);
            
            //Assert
            productsMock.Verify(v => v.Add(It.IsAny<Product>()),Times.Exactly(2));
        }

        [TestMethod]
        public void InvokeSaveChanges_WhenModelIsAdded()
        {
            //Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var productToImport = new ProductImportModel
            {
                Category = "TestCategory",
                Name = "TestProduct 2",
                PurchasePrice = 50m,
                Quantity = 5,
                Supplier = "TestSupplier"
            };
            var productsToImport = new List<ProductImportModel> { productToImport };
            var mockContext = new Mock<IOnlineStoreContext>();
            var productsMock = new List<Product>().GetQueryableMockDbSet();
            var categoriesMock = new List<Category> { new Category { Name = "TestCategory" } }.GetQueryableMockDbSet();
            var categoriesAddedMock = new List<Category> { new Category { Name = "Patkiii" } }.GetQueryableMockDbSet();
            var suppliersMock = new List<Supplier> { new Supplier { Name = "TestSupplier" } }.GetQueryableMockDbSet();
            var fakemapper = new Mock<IMapper>();
            fakemapper.Setup(x => x.Map<IProductImportModel, Product>(It.IsAny<IProductImportModel>())).Returns(new Product { Name = productToImport.Name, PurchasePrice = productToImport.PurchasePrice, SellingPrice = productToImport.PurchasePrice * 1.5m, Quantity = productToImport.Quantity });

            var fakeCategoryService = new Mock<ICategoryService>();
            mockContext.SetupSequence(s => s.Categories).Returns(categoriesMock.Object).Returns(categoriesAddedMock.Object);
            mockContext.Setup(s => s.Suppliers).Returns(suppliersMock.Object);
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            var service = new Logic.Services.ProductService(mockContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act
            service.AddProductRange(productsToImport);

            //Assert
            mockContext.Verify(v => v.SaveChanges(), Times.Once);
        }
    }
}
