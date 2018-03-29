using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OnlineStore.Tests.Services.JsonService
{
    [TestClass]
    public class DeserializeProducts_Should
    {
        [TestMethod]
        public void ThrowNullArgumentException_WhenInputStringIsNullOrEmpty()
        {
            //Arrange
            var jsonService = new Logic.Services.JsonService();
            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => jsonService.DeserializeProducts(null));
        }
    }
}
