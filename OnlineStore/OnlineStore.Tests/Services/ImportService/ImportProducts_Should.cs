using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Contracts;
using OnlineStore.DTO.ProductModels;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;
using OnlineStore.Tests.Mocks;

namespace OnlineStore.Tests.Services.ImportService
{
    [TestClass]
    public class ImportProducts_Should
    {
        [TestMethod]
        public void InvokeFileRead_ToReadImportString()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeJsonService = new Mock<IJsonService>();
            var mockFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            var importService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, mockFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            //Act
            importService.ExposedImportProductsFunction();

            //Assert
            mockFileReader.Verify(v => v.ReadAllText(It.IsAny<string>()), Times.Exactly(1));
        }

        [TestMethod]
        public void InvokeJsonread_ToDeserializeImportString()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var mockJsonService = new Mock<IJsonService>();
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            var importService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, mockJsonService.Object);
            //Act
            importService.ExposedImportProductsFunction();

            //Assert
            mockJsonService.Verify(v => v.DeserializeProducts(It.IsAny<string>()), Times.Exactly(1));
        }

        [TestMethod]
        public void InvokeValidator_ToValidateObject()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeJsonService = new Mock<IJsonService>();
            fakeJsonService.Setup(s => s.DeserializeProducts(It.IsAny<string>())).Returns(new ProductImportModel[1]{ new ProductImportModel()
            {
                Category = "test",
                Name = "test",
                PurchasePrice = 5.5m,
                Quantity = 1,
                Supplier = "Pesho"
            }});
            var fakeFileReader = new Mock<IFileReader>();
            var mockValidator = new Mock<IValidator>();
            var importService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, mockValidator.Object, fakeJsonService.Object);
            //Act
            importService.ExposedImportProductsFunction();

            //Assert
            mockValidator.Verify(v => v.IsValid(It.IsAny<object>()), Times.Exactly(1));
        }
    }
}
