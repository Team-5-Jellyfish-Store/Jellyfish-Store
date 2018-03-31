using System.Collections.Generic;
using System.Data.Entity;
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
    public class GetAllProducts_Should
    {
        [TestMethod]
        public void ReturnInstanceOfTypeIEnumerableProductModel_WhenInvoked()
        {
            // Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var mockContext = new Mock<IOnlineStoreContext>();
            var productsMock = new List<Product> { new Product { Name = "Testproduct", Quantity = 5 } }.GetQueryableMockDbSet();

            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);

            var service = new Logic.Services.ProductService(mockContext.Object, fakemapper.Object, fakeCategoryService.Object);

            // Act
            var products = service.GetAllProducts();

            //Assert
            Assert.IsInstanceOfType(products, typeof(IEnumerable<ProductModel>));
        }

        [TestMethod]
        public void ReturnCorrectData_WhenInvoked()
        {
            // Arrange
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

            // Act
            var productsGot = service.GetAllProducts();

            //Assert
            Assert.AreEqual(products.Count, productsGot.Count());
        }
    }
}
