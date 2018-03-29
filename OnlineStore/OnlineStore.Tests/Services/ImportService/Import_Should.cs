using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;
using OnlineStore.Tests.Mocks;

namespace OnlineStore.Tests.Services.ImportService
{
    [TestClass]
    public class Import_Should
    {
        [TestMethod]
        public void InvokeImportProductsImportCouriersAndImportSuppliers()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            var fakeJsonService = new Mock<IJsonService>();
            var mockImportService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            var expectedResult = "Suppliers method invoked!" + Environment.NewLine + "Courier method invoked!" + Environment.NewLine + "Products Method Invoked!";
            //Act
            var actualResult = mockImportService.Import();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
