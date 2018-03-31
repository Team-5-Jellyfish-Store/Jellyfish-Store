using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Providers.Providers;
using OnlineStore.DTO.Factory;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;
using System;

namespace OnlineStore.Tests.Commands.AddOrder
{
    [TestClass]
    public class Constructor_Should
    {
        private Mock<IOrderService> orderServiceStub;
        private Mock<IProductService> productServiceStub;
        private Mock<IUserSession> userSessionStub;
        private Mock<IDataTransferObjectFactory> dtoFactoryStub;
        private Mock<IValidator> validatorStub;
        private Mock<IWriter> writerStub;
        private Mock<IReader> readerStub;
        private Mock<DatetimeProvider> dateTimeStub;

        [TestInitialize]
        public void Initialize()
        {
            orderServiceStub = new Mock<IOrderService>();
            productServiceStub = new Mock<IProductService>();
            userSessionStub = new Mock<IUserSession>();
            dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            validatorStub = new Mock<IValidator>();
            writerStub = new Mock<IWriter>();
            readerStub = new Mock<IReader>();
            dateTimeStub = new Mock<DatetimeProvider>();
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_OrderService_IsNull()
        {
            // Arrange
            Action creatingAddOrderCmd = () => new AddOrderCommand(null, productServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, dateTimeStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingAddOrderCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_ProductService_IsNull()
        {
            // Arrange
            Action creatingAddOrderCmd = () => new AddOrderCommand(orderServiceStub.Object, null, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, dateTimeStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingAddOrderCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_UserSession_IsNull()
        {
            // Arrange
            Action creatingAddOrderCmd = () => new AddOrderCommand(orderServiceStub.Object, productServiceStub.Object, null, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, dateTimeStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingAddOrderCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_DTOFactory_IsNull()
        {
            // Arrange
            Action creatingAddOrderCmd = () => new AddOrderCommand(orderServiceStub.Object, productServiceStub.Object, userSessionStub.Object, null, validatorStub.Object, writerStub.Object, readerStub.Object, dateTimeStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingAddOrderCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Validator_IsNull()
        {
            // Arrange
            Action creatingAddOrderCmd = () => new AddOrderCommand(orderServiceStub.Object, productServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, null, writerStub.Object, readerStub.Object, dateTimeStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingAddOrderCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Writer_IsNull()
        {
            // Arrange
            Action creatingAddOrderCmd = () => new AddOrderCommand(orderServiceStub.Object, productServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, null, readerStub.Object, dateTimeStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingAddOrderCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Reader_IsNull()
        {
            // Arrange
            Action creatingAddOrderCmd = () => new AddOrderCommand(orderServiceStub.Object, productServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, null, dateTimeStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingAddOrderCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_DateTimeProvider_IsNull()
        {
            // Arrange
            Action creatingAddOrderCmd = () => new AddOrderCommand(orderServiceStub.Object, productServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingAddOrderCmd);
        }
    }
}
