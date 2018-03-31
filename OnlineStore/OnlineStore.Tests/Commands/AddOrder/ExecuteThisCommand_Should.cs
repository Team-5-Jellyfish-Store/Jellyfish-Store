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
            var userSessionStub = new Mock<IUserSession>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var dateTimeStub = new Mock<DatetimeProvider>();

            var addOrderCmd = new AddOrderCommand(orderServiceStub.Object, productServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, dateTimeStub.Object);

            userSessionStub.Setup(us => us.HasSomeoneLogged()).Returns(false);

            Action executingAddOrderCmd = () => addOrderCmd.ExecuteThisCommand();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingAddOrderCmd);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_ProductCount_IsNegative()
        {
            // Arrange
            var fakeProductName = "testProduct";
            var fakeProductCount = "-2";

            var orderServiceStub = new Mock<IOrderService>();
            var productServiceStub = new Mock<IProductService>();
            var userSessionStub = new Mock<IUserSession>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var dateTimeStub = new Mock<DatetimeProvider>();

            var productModelStub = new Mock<IProductModel>();

            var addOrderCmd = new AddOrderCommand(orderServiceStub.Object, productServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, dateTimeStub.Object);

            productModelStub.SetupGet(pm => pm.Name)
                .Returns(fakeProductName);

            userSessionStub.Setup(us => us.HasSomeoneLogged()).Returns(true);

            readerStub.SetupSequence(r => r.Read())
                .Returns(fakeProductName)
                .Returns(fakeProductCount);

            productServiceStub.Setup(ps => ps.FindProductByName(fakeProductName)).Returns(productModelStub.Object);

            Action executingAddOrderCmd = () => addOrderCmd.ExecuteThisCommand();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingAddOrderCmd);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_OrderModel_IsInvalid()
        {
            // Arrange
            var fakeProductName = "testProduct";
            var fakeProductCount = "4";
            var rejectMoreProducts = "n";

            var productModelStub = new Mock<IProductModel>();
            var orderModelStub = new Mock<IOrderMakeModel>();

            var orderServiceStub = new Mock<IOrderService>();
            var productServiceStub = new Mock<IProductService>();
            var userSessionStub = new Mock<IUserSession>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var dateTimeStub = new Mock<DatetimeProvider>();

            var addOrderCmd = new AddOrderCommand(orderServiceStub.Object, productServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, dateTimeStub.Object);

            productModelStub.SetupGet(pm => pm.Name)
                .Returns(fakeProductName);

            userSessionStub.Setup(us => us.HasSomeoneLogged()).Returns(true);

            readerStub.SetupSequence(r => r.Read())
                .Returns(fakeProductName)
                .Returns(fakeProductCount)
                .Returns(rejectMoreProducts);

            productServiceStub.Setup(ps => ps.FindProductByName(fakeProductName)).Returns(productModelStub.Object);

            dtoFactoryStub.Setup(dtoFac => dtoFac.CreateOrderMakeModel(It.IsAny<IDictionary<string, int>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(orderModelStub.Object);

            validatorStub.Setup(v => v.IsValid(orderModelStub.Object)).Returns(false);

            Action executingAddOrderCmd = () => addOrderCmd.ExecuteThisCommand();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingAddOrderCmd);
        }

        [TestMethod]
        public void Invoke_ReaderRead_ThreeTimes_PerProduct_PlusOneForComment()
        {
            // Arrange
            var fakeProductName = "testProduct";
            var fakeProductCount = "4";
            var rejectMoreProducts = "n";

            var productModelStub = new Mock<IProductModel>();
            var orderModelStub = new Mock<IOrderMakeModel>();

            var orderServiceStub = new Mock<IOrderService>();
            var productServiceStub = new Mock<IProductService>();
            var userSessionStub = new Mock<IUserSession>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var mockReader = new Mock<IReader>();
            var dateTimeStub = new Mock<DatetimeProvider>();

            var addOrderCmd = new AddOrderCommand(orderServiceStub.Object, productServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, mockReader.Object, dateTimeStub.Object);

            productModelStub.SetupGet(pm => pm.Name)
                .Returns(fakeProductName);

            userSessionStub.Setup(us => us.HasSomeoneLogged()).Returns(true);

            mockReader.SetupSequence(r => r.Read())
                .Returns(fakeProductName)
                .Returns(fakeProductCount)
                .Returns(rejectMoreProducts);

            productServiceStub.Setup(ps => ps.FindProductByName(fakeProductName)).Returns(productModelStub.Object);

            dtoFactoryStub.Setup(dtoFac => dtoFac.CreateOrderMakeModel(It.IsAny<IDictionary<string, int>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(orderModelStub.Object);

            validatorStub.Setup(v => v.IsValid(orderModelStub.Object)).Returns(true);

            // Act
            addOrderCmd.ExecuteThisCommand();

            // Assert
            mockReader.Verify(r => r.Read(), Times.Exactly(4));
        }

        [TestMethod]
        public void Invoke_UserSession_GetLoggedUser_When_Products_AreRead()
        {
            // Arrange
            var fakeProductName = "testProduct";
            var fakeProductCount = "4";
            var rejectMoreProducts = "n";

            var orderServiceStub = new Mock<IOrderService>();
            var productServiceStub = new Mock<IProductService>();
            var mockUserSession = new Mock<IUserSession>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var dateTimeStub = new Mock<DatetimeProvider>();

            var productModelStub = new Mock<IProductModel>();
            var orderModelStub = new Mock<IOrderMakeModel>();

            var addOrderCmd = new AddOrderCommand(orderServiceStub.Object, productServiceStub.Object, mockUserSession.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, dateTimeStub.Object);

            productModelStub.SetupGet(pm => pm.Name)
                .Returns(fakeProductName);

            mockUserSession.Setup(us => us.HasSomeoneLogged()).Returns(true);

            readerStub.SetupSequence(r => r.Read())
                .Returns(fakeProductName)
                .Returns(fakeProductCount)
                .Returns(rejectMoreProducts);

            productServiceStub.Setup(ps => ps.FindProductByName(fakeProductName)).Returns(productModelStub.Object);

            dtoFactoryStub.Setup(dtoFac => dtoFac.CreateOrderMakeModel(It.IsAny<IDictionary<string, int>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(orderModelStub.Object);

            validatorStub.Setup(v => v.IsValid(orderModelStub.Object)).Returns(true);

            // Act
            addOrderCmd.ExecuteThisCommand();

            // Assert
            mockUserSession.Verify(us => us.GetLoggedUserName(), Times.Once);
        }

        [TestMethod]
        public void Invoke_DTOFactory_CreateOrderMakeModel_With_CorrectValues()
        {
            // Arrange
            var fakeProductName = "testProduct";
            var fakeProductCount = "4";
            var rejectMoreProducts = "n";
            var fakeComment = "testComment";
            var fakeUsername = "testUser";
            var fakeOrderedOnDate = DateTime.Now;

            var orderServiceStub = new Mock<IOrderService>();
            var productServiceStub = new Mock<IProductService>();
            var userSessionStub = new Mock<IUserSession>();
            var mockDtoFactory = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var dateTimeStub = new Mock<DatetimeProvider>();

            var productModelStub = new Mock<IProductModel>();
            var orderModelStub = new Mock<IOrderMakeModel>();

            var addOrderCmd = new AddOrderCommand(orderServiceStub.Object, productServiceStub.Object, userSessionStub.Object, mockDtoFactory.Object, validatorStub.Object, writerStub.Object, readerStub.Object, dateTimeStub.Object);

            productModelStub
                .SetupGet(pm => pm.Name)
                .Returns(fakeProductName);

            userSessionStub
                .Setup(us => us.HasSomeoneLogged())
                .Returns(true);

            readerStub
                .SetupSequence(r => r.Read())
                .Returns(fakeProductName)
                .Returns(fakeProductCount)
                .Returns(rejectMoreProducts)
                .Returns(fakeComment);

            productServiceStub
                .Setup(ps => ps.FindProductByName(fakeProductName))
                .Returns(productModelStub.Object);

            userSessionStub
                .Setup(us => us.GetLoggedUserName())
                .Returns(fakeUsername);

            dateTimeStub
                .SetupGet(dt => dt.Now)
                .Returns(fakeOrderedOnDate);

            mockDtoFactory
                .Setup(dtoFac => dtoFac.CreateOrderMakeModel(It.IsAny<IDictionary<string, int>>(), fakeComment, fakeUsername, fakeOrderedOnDate))
                .Returns(orderModelStub.Object);

            validatorStub
                .Setup(v => v.IsValid(orderModelStub.Object))
                .Returns(true);

            // Act
            addOrderCmd.ExecuteThisCommand();

            // Assert
            mockDtoFactory.Verify(dtoFac => dtoFac.CreateOrderMakeModel(It.IsAny<IDictionary<string, int>>(), fakeComment, fakeUsername, fakeOrderedOnDate), Times.Once);
        }

        [TestMethod]
        public void Invoke_OrderService_MakeOrder_WithValid_OrderModel()
        {
            // Arrange
            var fakeProductName = "testProduct";
            var fakeProductCount = "4";
            var rejectMoreProducts = "n";

            var mockOrderService = new Mock<IOrderService>();
            var productServiceStub = new Mock<IProductService>();
            var userSessionStub = new Mock<IUserSession>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var dateTimeStub = new Mock<DatetimeProvider>();

            var productModelStub = new Mock<IProductModel>();
            var orderModelStub = new Mock<IOrderMakeModel>();

            var addOrderCmd = new AddOrderCommand(mockOrderService.Object, productServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, dateTimeStub.Object);

            productModelStub
                .SetupGet(pm => pm.Name)
                .Returns(fakeProductName);

            userSessionStub
                .Setup(us => us.HasSomeoneLogged())
                .Returns(true);

            readerStub
                .SetupSequence(r => r.Read())
                .Returns(fakeProductName)
                .Returns(fakeProductCount)
                .Returns(rejectMoreProducts);

            productServiceStub
                .Setup(ps => ps.FindProductByName(fakeProductName))
                .Returns(productModelStub.Object);

            dtoFactoryStub
                .Setup(dtoFac => dtoFac.CreateOrderMakeModel(It.IsAny<IDictionary<string, int>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(orderModelStub.Object);

            validatorStub
                .Setup(v => v.IsValid(orderModelStub.Object))
                .Returns(true);

            // Act
            addOrderCmd.ExecuteThisCommand();

            // Assert
            mockOrderService.Verify(os => os.MakeOrder(orderModelStub.Object), Times.Once);
        }

        [TestMethod]
        public void Adds_ProductNameAndCount_ToDictionaty_OfProductsAndCounts_When_CountIsValidAndProductExist()
        {
            // Arrange
            var fakeProductName = "testProduct";
            var fakeProductCount = "4";
            var rejectMoreProducts = "n";

            var orderServiceStub = new Mock<IOrderService>();
            var productServiceStub = new Mock<IProductService>();
            var userSessionStub = new Mock<IUserSession>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var dateTimeStub = new Mock<DatetimeProvider>();

            var productModelStub = new Mock<IProductModel>();
            var orderModelStub = new Mock<IOrderMakeModel>();

            var addOrderCmd = new MockAddOrderCommand(orderServiceStub.Object, productServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, dateTimeStub.Object);

            productModelStub
                .SetupGet(pm => pm.Name)
                .Returns(fakeProductName);

            userSessionStub
                .Setup(us => us.HasSomeoneLogged())
                .Returns(true);

            readerStub
                .SetupSequence(r => r.Read())
                .Returns(fakeProductName)
                .Returns(fakeProductCount)
                .Returns(rejectMoreProducts);

            productServiceStub
                .Setup(ps => ps.FindProductByName(fakeProductName))
                .Returns(productModelStub.Object);

            dtoFactoryStub
                .Setup(dtoFac => dtoFac.CreateOrderMakeModel(It.IsAny<IDictionary<string, int>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(orderModelStub.Object);

            validatorStub
                .Setup(v => v.IsValid(orderModelStub.Object))
                .Returns(true);

            // Act
            addOrderCmd.ExecuteThisCommand();

            var productsAndCountAfterExecuting = addOrderCmd.ExposedProductNameAndCounts;
            var isResultContainsProductName = productsAndCountAfterExecuting.ContainsKey(fakeProductName);
            var expectedProductCount = int.Parse(fakeProductCount);

            // Assert
            Assert.IsTrue(isResultContainsProductName);
            Assert.AreEqual(expectedProductCount, productsAndCountAfterExecuting[fakeProductName]);
        }
    }
}
