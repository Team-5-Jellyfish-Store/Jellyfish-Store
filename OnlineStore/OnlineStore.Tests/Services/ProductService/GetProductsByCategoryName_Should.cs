using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Common.AutoMapperConfig;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO.ProductModels;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using OnlineStore.Tests.Mocks;

namespace OnlineStore.Tests.Services.ProductService
{
    [TestClass]
    public class GetProductsByCategoryName_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenProvidedEmptyString()
        {
            //Arrange
            var fakeContext = new Mock<IOnlineStoreContext>();
            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();
            var service = new Logic.Services.ProductService(fakeContext.Object, fakemapper.Object,
                fakeCategoryService.Object);
            
            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => service.GetProductsByCategoryName(null));
        }

        [TestMethod]
        public void ReturnInstanceOfTypeIEnumerableProductModel_WhenInvoked()
        {
            // Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var mockContext = new Mock<IOnlineStoreContext>();
            var productsMock = new List<Product> { new Product { Name = "Testproduct", Quantity = 5, Category = new Category(){Name = "Patki"}} }.GetQueryableMockDbSet();

            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);

            var service = new Logic.Services.ProductService(mockContext.Object, fakemapper.Object, fakeCategoryService.Object);

            // Act
            var products = service.GetProductsByCategoryName("Patki");

            //Assert
            Assert.IsInstanceOfType(products, typeof(IEnumerable<ProductModel>));
        }

        [TestMethod]
        public void ReturnCorrectAmountOfProducts_WhenInvoked()
        {
            // Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var mockContext = new Mock<IOnlineStoreContext>();
            var productsMock = new List<Product> { new Product { Name = "Testproduct", Quantity = 5, Category = new Category() { Name = "Patki" } } }.GetQueryableMockDbSet();

            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);

            var service = new Logic.Services.ProductService(mockContext.Object, fakemapper.Object, fakeCategoryService.Object);

            // Act
            var products = service.GetProductsByCategoryName("Patki");

            //Assert
            Assert.AreEqual(1, products.Count());
        }
    }
}
