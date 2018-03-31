using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Tests.Services.OrderService
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnInstance_WhenProvidedCorrectParameters()
        {
            //Arrange
            var fakeContext = new Mock<IOnlineStoreContext>();

            //Act && Assert
            Assert.IsInstanceOfType(new Logic.Services.OrderService(fakeContext.Object), typeof(IOrderService));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenContextIsNull()
        {
            //Arrange && Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Logic.Services.OrderService(null));
        }
    }
}
