using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OnlineStore.Tests.Services.JsonService
{
    [TestClass]
    public class DeserializeSuppliers_Should
    {
        [TestMethod]
        public void ThrowNullArgumentException_WhenInputStringIsNullOrEmpty()
        {
            //Arrange
            var jsonService = new Logic.Services.JsonService();
            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => jsonService.DeserializeSuppliers(null));
        }
    }
}
