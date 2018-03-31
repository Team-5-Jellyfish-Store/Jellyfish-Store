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
    public class ProductExistsByName_Should
    {
        [TestMethod]
        public void ThrowNullArgumentException_WhenProductNameIsNull()
        {
            //Arrange
            var fakeContext = new Mock<IOnlineStoreContext>();
            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();
            var service = new Logic.Services.ProductService(fakeContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => service.ProductExistsByName(null));
        }

        [TestMethod]
        public void ReturnTrue_WhenProductExists()
        { 
            //Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var mockContext = new Mock<IOnlineStoreContext>();
            var productsMock = new List<Product> { new Product { Name = "Pesho" } }.GetQueryableMockDbSet();
            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            var service = new Logic.Services.ProductService(mockContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act
            var searchResult = service.ProductExistsByName("Pesho");

            //Assert
            Assert.IsTrue(searchResult);
        }

        [TestMethod]
        public void ReturnFalse_WhenProductDoesNotExist()
        {
            //Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var mockContext = new Mock<IOnlineStoreContext>();
            var productsMock = new List<Product> { new Product { Name = "Pesho" } }.GetQueryableMockDbSet();
            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            var service = new Logic.Services.ProductService(mockContext.Object, fakemapper.Object, fakeCategoryService.Object);

            //Act
            var searchResult = service.ProductExistsByName("Pesho go nqma");

            //Assert
            Assert.IsFalse(searchResult);
        }
    }
}
