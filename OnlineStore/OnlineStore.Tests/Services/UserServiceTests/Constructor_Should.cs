using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Logic.Services;
using System;

namespace OnlineStore.Tests.Services.UserServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        private Mock<IOnlineStoreContext> ctxStub;
        private Mock<IMapper> mapperStub;
        private Mock<ITownService> townServiceStub;
        private Mock<IAddressService> addressServiceStub;

        [TestInitialize]
        public void Initialize()
        {
            ctxStub = new Mock<IOnlineStoreContext>();
            mapperStub = new Mock<IMapper>();
            townServiceStub = new Mock<ITownService>();
            addressServiceStub = new Mock<IAddressService>();
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_OnlineStoreContext_InNull()
        {
            // Arrange
            Action creatingUserService = () => new UserService(null, mapperStub.Object, townServiceStub.Object, addressServiceStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingUserService);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Mapper_InNull()
        {
            // Arrange
            Action creatingUserService = () => new UserService(ctxStub.Object, null, townServiceStub.Object, addressServiceStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingUserService);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_TownService_InNull()
        {
            // Arrange
            Action creatingUserService = () => new UserService(ctxStub.Object, mapperStub.Object, null, addressServiceStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingUserService);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_AddressService_InNull()
        {
            // Arrange
            Action creatingUserService = () => new UserService(ctxStub.Object, mapperStub.Object, townServiceStub.Object, null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingUserService);
        }
    }
}
