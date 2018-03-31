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
    public class RemoveProductByName_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenInputStringIsNull()
        {
            //Arrange
            var fakeContext = new Mock<IOnlineStoreContext>();
            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();
            var service = new Logic.Services.ProductService(fakeContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => service.RemoveProductByName(null));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenProductNotFound()
        {
            //Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var mockContext = new Mock<IOnlineStoreContext>();
            var productsMock = new List<Product> { new Product { Name = "Testproduct", Quantity = 5, Category = new Category { Name = "Patki" } } }.GetQueryableMockDbSet();
            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            var service = new Logic.Services.ProductService(mockContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act && Assert
            Assert.ThrowsException<ArgumentException>(() => service.RemoveProductByName("Patka"));
        }

        [TestMethod]
        public void InvokeRemoveMethod_WhenProductIsFound()
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
            service.RemoveProductByName("Testproduct");

            //Assert
            productsMock.Verify(v => v.Remove(It.IsNotNull<Product>()), Times.Once);
        }

        [TestMethod]
        public void InvokeSaveChanges_WhenProductIsAvailableAndRemoved()
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
            service.RemoveProductByName("Testproduct");

            //Assert
            mockContext.Verify(v => v.SaveChanges(), Times.Once);
        }
    }
}
