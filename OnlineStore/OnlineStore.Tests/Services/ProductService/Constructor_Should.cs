using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Tests.Services.ProductService
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnInstance_WhenProvidedCorrectParameters()
        {
            //Arrange
            var fakeContext = new Mock<IOnlineStoreContext>();
            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();

            //Act && Assert
            Assert.IsInstanceOfType(new Logic.Services.ProductService(fakeContext.Object, fakemapper.Object, fakeCategoryService.Object), typeof(IProductService));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenContextIsNull()
        {
            //Arrange
            var fakemapper = new Mock<IMapper>();
            var fakeCategoryService = new Mock<ICategoryService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Logic.Services.ProductService(null, fakemapper.Object, fakeCategoryService.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenMapperIsNull()
        {
            //Arrange
            var fakeContext = new Mock<IOnlineStoreContext>();
            var fakeCategoryService = new Mock<ICategoryService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Logic.Services.ProductService(fakeContext.Object, null, fakeCategoryService.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenCategoryServiceIsNull()
        {
            //Arrange
            var fakeContext = new Mock<IOnlineStoreContext>();
            var fakemapper = new Mock<IMapper>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Logic.Services.ProductService(fakeContext.Object, fakemapper.Object, null));
        }
    }
}