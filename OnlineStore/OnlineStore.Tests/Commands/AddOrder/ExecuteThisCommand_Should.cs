using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Providers.Providers;
using OnlineStore.DTO.Factory;
using OnlineStore.DTO.OrderModels.Constracts;
using OnlineStore.DTO.ProductModels.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;
using OnlineStore.Tests.Mocks;
using System;
using System.Collections.Generic;

namespace OnlineStore.Tests.Commands.AddOrder
{
    [TestClass]
    public class ExecuteThisCommand_Should
    {
        [TestMethod]
        public void Throw_ArgumentException_When_NoUser_IsLoggedIn()
        {
            // Arrange
            var orderServiceStub = new Mock<IOrderService>();
            var productServiceStub = new Mock<IProductService>();
            var userSessionMock = new Mock<IUserSession>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var dateTimeStub = new Mock<DatetimeProvider>();

            var addOrderCmd = new AddOrderCommand(orderServiceStub.Object, productServiceStub.Object, userSessionMock.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, dateTimeStub.Object);

            userSessionMock.Setup(us => us.HasSomeoneLogged()).Returns(false);

            Action executingAddOrderCmd = () => addOrderCmd.ExecuteThisCommand();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingAddOrderCmd);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_ProductCount_IsNegative()
        {
            // Arrange
            var productName = "testProduct";
            var productCount = "-2";

            var orderServiceStub = new Mock<IOrderService>();
            var productServiceMock = new Mock<IProductService>();
            var userSessionMock = new Mock<IUserSession>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var dateTimeStub = new Mock<DatetimeProvider>();

            var productModelMock = new Mock<IProductModel>();

            var addOrderCmd = new AddOrderCommand(orderServiceStub.Object, productServiceMock.Object, userSessionMock.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerMock.Object, dateTimeStub.Object);

            productModelMock.SetupGet(pm => pm.Name)
                .Returns(productName);

            userSessionMock.Setup(us => us.HasSomeoneLogged()).Returns(true);

            readerMock.SetupSequence(r => r.Read())
                .Returns(productName)
                .Returns(productCount);

            productServiceMock.Setup(ps => ps.FindProductByName(productName)).Returns(productModelMock.Object);

            Action executingAddOrderCmd = () => addOrderCmd.ExecuteThisCommand();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingAddOrderCmd);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_OrderModel_IsInvalid()
        {
            // Arrange
            var productName = "testProduct";
            var productCount = "4";
            var rejectMoreProducts = "n";

            var orderServiceStub = new Mock<IOrderService>();
            var productServiceMock = new Mock<IProductService>();
            var userSessionMock = new Mock<IUserSession>();
            var dtoFactoryMock = new Mock<IDataTransferObjectFactory>();
            var validatorMock = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var dateTimeStub = new Mock<DatetimeProvider>();

            var productModelMock = new Mock<IProductModel>();
            var orderModelStub = new Mock<IOrderMakeModel>();

            var addOrderCmd = new AddOrderCommand(orderServiceStub.Object, productServiceMock.Object, userSessionMock.Object, dtoFactoryMock.Object, validatorMock.Object, writerStub.Object, readerMock.Object, dateTimeStub.Object);

            productModelMock.SetupGet(pm => pm.Name)
                .Returns(productName);

            userSessionMock.Setup(us => us.HasSomeoneLogged()).Returns(true);

            readerMock.SetupSequence(r => r.Read())
                .Returns(productName)
                .Returns(productCount)
                .Returns(rejectMoreProducts);

            productServiceMock.Setup(ps => ps.FindProductByName(productName)).Returns(productModelMock.Object);

            dtoFactoryMock.Setup(dtoFac => dtoFac.CreateOrderMakeModel(It.IsAny<IDictionary<string, int>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(orderModelStub.Object);

            validatorMock.Setup(v => v.IsValid(orderModelStub.Object)).Returns(false);

            Action executingAddOrderCmd = () => addOrderCmd.ExecuteThisCommand();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingAddOrderCmd);
        }

        [TestMethod]
        public void Invoke_ReaderRead_ThreeTimes_PerProduct_PlusOneForComment()
        {
            // Arrange
            var productName = "testProduct";
            var productCount = "4";
            var rejectMoreProducts = "n";

            var orderServiceStub = new Mock<IOrderService>();
            var productServiceMock = new Mock<IProductService>();
            var userSessionMock = new Mock<IUserSession>();
            var dtoFactoryMock = new Mock<IDataTransferObjectFactory>();
            var validatorMock = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var dateTimeStub = new Mock<DatetimeProvider>();

            var productModelMock = new Mock<IProductModel>();
            var orderModelStub = new Mock<IOrderMakeModel>();

            var addOrderCmd = new AddOrderCommand(orderServiceStub.Object, productServiceMock.Object, userSessionMock.Object, dtoFactoryMock.Object, validatorMock.Object, writerStub.Object, readerMock.Object, dateTimeStub.Object);

            productModelMock.SetupGet(pm => pm.Name)
                .Returns(productName);

            userSessionMock.Setup(us => us.HasSomeoneLogged()).Returns(true);

            readerMock.SetupSequence(r => r.Read())
                .Returns(productName)
                .Returns(productCount)
                .Returns(rejectMoreProducts);

            productServiceMock.Setup(ps => ps.FindProductByName(productName)).Returns(productModelMock.Object);

            dtoFactoryMock.Setup(dtoFac => dtoFac.CreateOrderMakeModel(It.IsAny<IDictionary<string, int>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(orderModelStub.Object);

            validatorMock.Setup(v => v.IsValid(orderModelStub.Object)).Returns(true);

            // Act
            addOrderCmd.ExecuteThisCommand();

            // Assert
            readerMock.Verify(r => r.Read(), Times.Exactly(4));
        }

        [TestMethod]
        public void Invoke_UserSession_GetLoggedUser_When_Products_AreRead()
        {
            // Arrange
            var productName = "testProduct";
            var productCount = "4";
            var rejectMoreProducts = "n";

            var orderServiceStub = new Mock<IOrderService>();
            var productServiceMock = new Mock<IProductService>();
            var userSessionMock = new Mock<IUserSession>();
            var dtoFactoryMock = new Mock<IDataTransferObjectFactory>();
            var validatorMock = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var dateTimeStub = new Mock<DatetimeProvider>();

            var productModelMock = new Mock<IProductModel>();
            var orderModelStub = new Mock<IOrderMakeModel>();

            var addOrderCmd = new AddOrderCommand(orderServiceStub.Object, productServiceMock.Object, userSessionMock.Object, dtoFactoryMock.Object, validatorMock.Object, writerStub.Object, readerMock.Object, dateTimeStub.Object);

            productModelMock.SetupGet(pm => pm.Name)
                .Returns(productName);

            userSessionMock.Setup(us => us.HasSomeoneLogged()).Returns(true);

            readerMock.SetupSequence(r => r.Read())
                .Returns(productName)
                .Returns(productCount)
                .Returns(rejectMoreProducts);

            productServiceMock.Setup(ps => ps.FindProductByName(productName)).Returns(productModelMock.Object);

            dtoFactoryMock.Setup(dtoFac => dtoFac.CreateOrderMakeModel(It.IsAny<IDictionary<string, int>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(orderModelStub.Object);

            validatorMock.Setup(v => v.IsValid(orderModelStub.Object)).Returns(true);

            // Act
            addOrderCmd.ExecuteThisCommand();

            // Assert
            userSessionMock.Verify(us => us.GetLoggedUserName(), Times.Once);
        }

        [TestMethod]
        public void Invoke_DTOFactory_CreateOrderMakeModel_With_CorrectValues()
        {
            // Arrange
            var productName = "testProduct";
            var productCount = "4";
            var rejectMoreProducts = "n";
            var comment = "testComment";
            var username = "testUser";
            var orderedOn = DateTime.Now;

            var orderServiceStub = new Mock<IOrderService>();
            var productServiceMock = new Mock<IProductService>();
            var userSessionMock = new Mock<IUserSession>();
            var dtoFactoryMock = new Mock<IDataTransferObjectFactory>();
            var validatorMock = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var dateTimeMock = new Mock<DatetimeProvider>();

            var productModelMock = new Mock<IProductModel>();
            var orderModelStub = new Mock<IOrderMakeModel>();

            var addOrderCmd = new AddOrderCommand(orderServiceStub.Object, productServiceMock.Object, userSessionMock.Object, dtoFactoryMock.Object, validatorMock.Object, writerStub.Object, readerMock.Object, dateTimeMock.Object);

            productModelMock
                .SetupGet(pm => pm.Name)
                .Returns(productName);

            userSessionMock
                .Setup(us => us.HasSomeoneLogged())
                .Returns(true);

            readerMock
                .SetupSequence(r => r.Read())
                .Returns(productName)
                .Returns(productCount)
                .Returns(rejectMoreProducts)
                .Returns(comment);

            productServiceMock
                .Setup(ps => ps.FindProductByName(productName))
                .Returns(productModelMock.Object);

            userSessionMock
                .Setup(us => us.GetLoggedUserName())
                .Returns(username);

            dateTimeMock
                .SetupGet(dt => dt.Now)
                .Returns(orderedOn);

            dtoFactoryMock
                .Setup(dtoFac => dtoFac.CreateOrderMakeModel(It.IsAny<IDictionary<string, int>>(), comment, username, orderedOn))
                .Returns(orderModelStub.Object);

            validatorMock
                .Setup(v => v.IsValid(orderModelStub.Object))
                .Returns(true);

            // Act
            addOrderCmd.ExecuteThisCommand();

            // Assert
            dtoFactoryMock.Verify(dtoFac => dtoFac.CreateOrderMakeModel(It.IsAny<IDictionary<string, int>>(), comment, username, orderedOn), Times.Once);
        }

        [TestMethod]
        public void Invoke_OrderService_MakeOrder_WithValid_OrderModel()
        {
            // Arrange
            var productName = "testProduct";
            var productCount = "4";
            var rejectMoreProducts = "n";

            var orderServiceMock = new Mock<IOrderService>();
            var productServiceMock = new Mock<IProductService>();
            var userSessionMock = new Mock<IUserSession>();
            var dtoFactoryMock = new Mock<IDataTransferObjectFactory>();
            var validatorMock = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var dateTimeStub = new Mock<DatetimeProvider>();

            var productModelMock = new Mock<IProductModel>();
            var orderModelStub = new Mock<IOrderMakeModel>();

            var addOrderCmd = new AddOrderCommand(orderServiceMock.Object, productServiceMock.Object, userSessionMock.Object, dtoFactoryMock.Object, validatorMock.Object, writerStub.Object, readerMock.Object, dateTimeStub.Object);

            productModelMock
                .SetupGet(pm => pm.Name)
                .Returns(productName);

            userSessionMock
                .Setup(us => us.HasSomeoneLogged())
                .Returns(true);

            readerMock
                .SetupSequence(r => r.Read())
                .Returns(productName)
                .Returns(productCount)
                .Returns(rejectMoreProducts);

            productServiceMock
                .Setup(ps => ps.FindProductByName(productName))
                .Returns(productModelMock.Object);

            dtoFactoryMock
                .Setup(dtoFac => dtoFac.CreateOrderMakeModel(It.IsAny<IDictionary<string, int>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(orderModelStub.Object);

            validatorMock
                .Setup(v => v.IsValid(orderModelStub.Object))
                .Returns(true);

            // Act
            addOrderCmd.ExecuteThisCommand();

            // Assert
            orderServiceMock.Verify(os => os.MakeOrder(orderModelStub.Object), Times.Once);
        }
    }
}
