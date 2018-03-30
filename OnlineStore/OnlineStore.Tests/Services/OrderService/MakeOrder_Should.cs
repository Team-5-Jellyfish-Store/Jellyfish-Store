using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Data.Contracts;

namespace OnlineStore.Tests.Services.OrderService
{
    [TestClass]
    public class MakeOrder_Should
    {
        [TestMethod]
        public void ThrowArgumentException_WhenOrderModelIsNull()
        {
            //Arrange
            var fakeContext = new Mock<IOnlineStoreContext>();
            var orderModel = new Logic.Services.OrderService(fakeContext.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => orderModel.MakeOrder(null));
        }
    }
}
