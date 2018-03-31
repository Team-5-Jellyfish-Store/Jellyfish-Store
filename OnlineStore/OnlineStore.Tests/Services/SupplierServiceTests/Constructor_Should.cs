using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Logic.Services;
using System;

namespace OnlineStore.Tests.Services.SupplierServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        private Mock<IOnlineStoreContext> ctxStub;
        private Mock<IAddressService> addressServiceStub;
        private Mock<ITownService> townServiceStub;
        private Mock<IMapper> mapperStub;

        [TestInitialize]
        public void Initialize()
        {
            ctxStub = new Mock<IOnlineStoreContext>();
            addressServiceStub = new Mock<IAddressService>();
            townServiceStub = new Mock<ITownService>();
            mapperStub = new Mock<IMapper>();
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_OnlineStoreContext_InNull()
        {
            // Arrange
            Action creatingSupplierService = () => new SupplierService(null, addressServiceStub.Object, townServiceStub.Object, mapperStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingSupplierService);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_AddressService_InNull()
        {
            // Arrange
            Action creatingSupplierService = () => new SupplierService(ctxStub.Object, null, townServiceStub.Object, mapperStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingSupplierService);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_TownService_InNull()
        {
            // Arrange
            Action creatingSupplierService = () => new SupplierService(ctxStub.Object, addressServiceStub.Object, null, mapperStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingSupplierService);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Mapper_InNull()
        {
            // Arrange
            Action creatingSupplierService = () => new SupplierService(ctxStub.Object, addressServiceStub.Object, townServiceStub.Object, null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingSupplierService);
        }
    }
}
