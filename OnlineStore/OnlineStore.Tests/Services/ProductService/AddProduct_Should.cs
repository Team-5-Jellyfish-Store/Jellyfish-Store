using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class AddProduct_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenModelIsNull()
        {
            //Arrange
            var fakeContext = new Mock<IOnlineStoreContext>();
            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();
            var service = new Logic.Services.ProductService(fakeContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => service.AddProduct(null));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenProductExists()
        {
            //Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var mockContext = new Mock<IOnlineStoreContext>();
            var products = new List<Product> { new Product { Name = "Testproduct", Quantity = 5, Category = new Category { Name = "Patki" } } };
            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();
            var mockSet = new Mock<DbSet<Product>>();
            mockSet.SetupData(products);
            mockContext.Setup(s => s.Products).Returns(mockSet.Object);
            var service = new Logic.Services.ProductService(mockContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act

            var productToAdd = new ProductImportModel { Name = "Testproduct" };

            //Assert
            Assert.ThrowsException<ArgumentException>(() => service.AddProduct(productToAdd));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenCategoryNotFound()
        {
            //Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var mockContext = new Mock<IOnlineStoreContext>();
            var products = new List<Product> { new Product { Name = "Testproduct", Quantity = 5, Category = new Category { Name = "Patki" } } };
            var categoriesMock = new List<Category> { new Category { Name = "TestCategory" } }.GetQueryableMockDbSet();
            var suppliersMock = new List<Supplier> { new Supplier { Name = "TestSupplier" } }.GetQueryableMockDbSet();
            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();
            var mockSet = new Mock<DbSet<Product>>();
            mockSet.SetupData(products);
            mockContext.Setup(s => s.Products).Returns(mockSet.Object);
            mockContext.Setup(s => s.Categories).Returns(categoriesMock.Object);
            mockContext.Setup(s => s.Suppliers).Returns(suppliersMock.Object);
            var service = new Logic.Services.ProductService(mockContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act

            var productToAdd = new ProductImportModel { Name = "Testproduct2", Category = "Gubi" };

            //Assert
            Assert.ThrowsException<ArgumentException>(() => service.AddProduct(productToAdd));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenSupplierNotFound()
        {
            //Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var mockContext = new Mock<IOnlineStoreContext>();
            var products = new List<Product> { new Product { Name = "Testproduct", Quantity = 5, Category = new Category { Name = "Patki" } } };
            var categoriesMock = new List<Category> { new Category { Name = "TestCategory" } }.GetQueryableMockDbSet();
            var suppliersMock = new List<Supplier> { new Supplier { Name = "TestSupplier" } }.GetQueryableMockDbSet();
            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();
            var mockSet = new Mock<DbSet<Product>>();
            mockSet.SetupData(products);
            mockContext.Setup(s => s.Products).Returns(mockSet.Object);
            mockContext.Setup(s => s.Categories).Returns(categoriesMock.Object);
            mockContext.Setup(s => s.Suppliers).Returns(suppliersMock.Object);
            var service = new Logic.Services.ProductService(mockContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act

            var productToAdd = new ProductImportModel { Name = "Testproduct2", Supplier = "Chushki", Category = "TestCategory" };

            //Assert
            Assert.ThrowsException<ArgumentException>(() => service.AddProduct(productToAdd));
        }

        [TestMethod]
        public void InvokeAddMethod_WhenModelIsAcceptable()
        {
            //Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var productToAdd = new ProductImportModel
            {
                Name = "Testproduct2",
                Supplier = "TestSupplier",
                Category = "TestCategory",
                PurchasePrice = 5.50m,
                Quantity = 5
            };
            var mockContext = new Mock<IOnlineStoreContext>();
            var productsMock = new List<Product> { new Product { Name = "Testproduct", Quantity = 5, Category = new Category { Name = "Patki" } } }.GetQueryableMockDbSet();
            var categoriesMock = new List<Category> { new Category { Name = "TestCategory" } }.GetQueryableMockDbSet();
            var suppliersMock = new List<Supplier> { new Supplier { Name = "TestSupplier" } }.GetQueryableMockDbSet();
            var fakemapper = new Mock<IMapper>();
            fakemapper.Setup(x => x.Map<Product>(It.IsAny<IProductImportModel>())).Returns(new Product { Name = productToAdd.Name, PurchasePrice = productToAdd.PurchasePrice, SellingPrice = productToAdd.PurchasePrice * 1.5m });

            var fakeCategoryService = new Mock<ICategoryService>();
            mockContext.Setup(s => s.Categories).Returns(categoriesMock.Object);
            mockContext.Setup(s => s.Suppliers).Returns(suppliersMock.Object);
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            var service = new Logic.Services.ProductService(mockContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act
            service.AddProduct(productToAdd);

            //Assert
            productsMock.Verify(v => v.Add(It.IsNotNull<Product>()), Times.Once);
        }

        [TestMethod]
        public void InvokeSaveChanges_WhenModelIsAcceptable()
        {
            //Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var productToAdd = new ProductImportModel
            {
                Name = "Testproduct2",
                Supplier = "TestSupplier",
                Category = "TestCategory",
                PurchasePrice = 5.50m,
                Quantity = 5
            };
            var mockContext = new Mock<IOnlineStoreContext>();
            var productsMock = new List<Product> { new Product { Name = "Testproduct", Quantity = 5, Category = new Category { Name = "Patki" } } }.GetQueryableMockDbSet();
            var categoriesMock = new List<Category> { new Category { Name = "TestCategory" } }.GetQueryableMockDbSet();
            var suppliersMock = new List<Supplier> { new Supplier { Name = "TestSupplier" } }.GetQueryableMockDbSet();
            var fakemapper = new Mock<IMapper>();
            fakemapper.Setup(x => x.Map<Product>(It.IsAny<IProductImportModel>())).Returns(new Product { Name = productToAdd.Name, PurchasePrice = productToAdd.PurchasePrice, SellingPrice = productToAdd.PurchasePrice * 1.5m });

            var fakeCategoryService = new Mock<ICategoryService>();

            mockContext.Setup(s => s.Categories).Returns(categoriesMock.Object);
            mockContext.Setup(s => s.Suppliers).Returns(suppliersMock.Object);
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            var service = new Logic.Services.ProductService(mockContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act
            service.AddProduct(productToAdd);

            //Assert
            mockContext.Verify(v => v.SaveChanges(), Times.Once);
        }
    }
}
