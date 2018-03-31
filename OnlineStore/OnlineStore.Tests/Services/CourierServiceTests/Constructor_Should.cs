using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Logic.Services;
using System;

namespace OnlineStore.Tests.Services.CourierServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        private Mock<IOnlineStoreContext> ctxStub;
        private Mock<ITownService> townServiceStub;
        private Mock<IAddressService> addressServiceStub;
        private Mock<IMapper> mapperStub;

        [TestInitialize]
        public void Initialize()
        {
            ctxStub = new Mock<IOnlineStoreContext>();
            townServiceStub = new Mock<ITownService>();
            addressServiceStub = new Mock<IAddressService>();
            mapperStub = new Mock<IMapper>();
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_OnlineStoreContext_InNull()
        {
            // Arrange
            Action creatingCourierService = () => new CourierService(null, townServiceStub.Object, addressServiceStub.Object, mapperStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingCourierService);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_TownService_InNull()
        {
            // Arrange
            Action creatingCourierService = () => new CourierService(ctxStub.Object, null, addressServiceStub.Object, mapperStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingCourierService);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_AddressService_InNull()
        {
            // Arrange
            Action creatingCourierService = () => new CourierService(ctxStub.Object, townServiceStub.Object, null, mapperStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingCourierService);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Mapper_InNull()
        {
            // Arrange
            Action creatingCourierService = () => new CourierService(ctxStub.Object, townServiceStub.Object, addressServiceStub.Object, null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingCourierService);
        }
    }
}
