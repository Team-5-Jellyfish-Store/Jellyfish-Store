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
    public class FindProductByName_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenNameIsNull()
        {
            //Arrange
            var fakeContext = new Mock<IOnlineStoreContext>();
            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();
            var service = new Logic.Services.ProductService(fakeContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => service.FindProductByName(null));
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
            Assert.ThrowsException<ArgumentException>(() => service.FindProductByName("Patka"));
        }

        [TestMethod]
        public void ReturnInstanceOfTypeIProductModel_WhenProductExists()
        {
            //Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var mockContext = new Mock<IOnlineStoreContext>();
            var productToAdd = new Product
            {
                Name = "Testproduct",
                Quantity = 5,
                Category = new Category {Name = "Patki"},
                Supplier = new Supplier() {Name = "Pesho"},
                PurchasePrice = 50m,
                SellingPrice = 75m
            };
            var productsMock = new List<Product> { productToAdd}.GetQueryableMockDbSet();
            var fakemapper = new Mock<IMapper>();
            fakemapper.Setup(x => x.Map<IProductModel>(It.IsAny<Product>())).Returns(new ProductModel { Name = productToAdd.Name, PurchasePrice = productToAdd.PurchasePrice, SellingPrice = productToAdd.PurchasePrice * 1.5m, Quantity = productToAdd.Quantity, CategoryName = productToAdd.Category.Name});
            var fakeCategoryService = new Mock<ICategoryService>();
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            var service = new Logic.Services.ProductService(mockContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act
            var productFound = service.FindProductByName("Testproduct");
            
            // Assert
            Assert.IsInstanceOfType(productFound, typeof(IProductModel));
        }
    }
}
