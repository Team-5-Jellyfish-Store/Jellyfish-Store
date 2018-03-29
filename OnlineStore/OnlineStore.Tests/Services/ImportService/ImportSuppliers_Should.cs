using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Contracts;
using OnlineStore.DTO.SupplierModels;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;
using OnlineStore.Tests.Mocks;

namespace OnlineStore.Tests.Services.ImportService
{
    [TestClass]
    public class ImportSuppliers_Should
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
            importService.ExposedImportSuppliersFunction();

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
            importService.ExposedImportSuppliersFunction();

            //Assert
            mockJsonService.Verify(v => v.DeserializeSuppliers(It.IsAny<string>()), Times.Exactly(1));
        }

        [TestMethod]
        public void InvokeValidator_ToValidateObject()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeJsonService = new Mock<IJsonService>();
            fakeJsonService.Setup(s => s.DeserializeSuppliers(It.IsAny<string>())).Returns(new[] { new SuppliersImportModel() });
            var fakeFileReader = new Mock<IFileReader>();
            var mockValidator = new Mock<IValidator>();
            var importService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, mockValidator.Object, fakeJsonService.Object);
            //Act
            importService.ExposedImportSuppliersFunction();

            //Assert
            mockValidator.Verify(v => v.IsValid(It.IsAny<object>()), Times.Exactly(1));
        }

        [TestMethod]
        public void ReturnInvalidInputMessage_WhenModelIsInvalid()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeJsonService = new Mock<IJsonService>();
            fakeJsonService.Setup(s => s.DeserializeSuppliers(It.IsAny<string>())).Returns(new[] { new SuppliersImportModel() });
            var fakeFileReader = new Mock<IFileReader>();
            var mockValidator = new Mock<IValidator>();
            mockValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(false);
            var importService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, mockValidator.Object, fakeJsonService.Object);
            string expectedMessage = "Import rejected. Input is with invalid format.\r\n";
            //Act
            string actualMessage = importService.ExposedImportSuppliersFunction();

            //Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void InvokeSupplierExistsByName_WhenModelIsValid()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var mockSupplierService = new Mock<ISupplierService>();
            var fakeJsonService = new Mock<IJsonService>();
            fakeJsonService.Setup(s => s.DeserializeSuppliers(It.IsAny<string>())).Returns(new[] { new SuppliersImportModel() });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(true);
            var importService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                mockSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            //Act
            importService.ExposedImportSuppliersFunction();

            //Assert
            mockSupplierService.Verify(v => v.SupplierExistsByName(It.IsAny<string>()), Times.Exactly(1));
        }

        [TestMethod]
        public void ReturnsCorrectMessage_WhenModelIsValidAndSupplierExists()
        {
            //Arrange
            var fakekProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var mockSupplierService = new Mock<ISupplierService>();
            mockSupplierService.Setup(s => s.SupplierExistsByName(It.IsAny<string>())).Returns(true);
            var fakeJsonService = new Mock<IJsonService>();
            var fakeSupplier = new SuppliersImportModel
            {
                Name = "Pesho"
            };
            fakeJsonService.Setup(s => s.DeserializeSuppliers(It.IsAny<string>())).Returns(new[] { fakeSupplier });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(true);
            var importService = new MockImportService(fakekProductService.Object, fakeCourierService.Object,
                mockSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            string expectedMessage = $"Supplier {fakeSupplier.Name} already exists!\r\n";
            //Act
            string actualMessage = importService.ExposedImportSuppliersFunction();

            //Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void AddsTheSupplierToListOfValidSuppliers_WhenModelIsValidAndSupplierDoesNotExist()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var mockSupplierService = new Mock<ISupplierService>();
            mockSupplierService.Setup(s => s.SupplierExistsByName(It.IsAny<string>())).Returns(false);
            var fakeJsonService = new Mock<IJsonService>();
            var fakeSupplier = new SuppliersImportModel
            {
                Name = "Pesho",
            };
            fakeJsonService.Setup(s => s.DeserializeSuppliers(It.IsAny<string>())).Returns(new[] { fakeSupplier });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(true);
            var importService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                mockSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);

            //Act
            importService.ExposedImportSuppliersFunction();

            //Assert
            Assert.IsTrue(importService.ExposedValidSuppliers.Any(a => a.Name == fakeSupplier.Name));
        }

        [TestMethod]
        public void ReturnsValidMessage_WhenSupplierIsAdded()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var mockSupplierService = new Mock<ISupplierService>();
            mockSupplierService.Setup(s => s.SupplierExistsByName(It.IsAny<string>())).Returns(false);
            var fakeJsonService = new Mock<IJsonService>();
            var fakeSupplier = new SuppliersImportModel
            {
                Name = "Pesho"
            };
            fakeJsonService.Setup(s => s.DeserializeSuppliers(It.IsAny<string>())).Returns(new[] { fakeSupplier });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(true);
            var importService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                mockSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            string expectedMessage = $"Supplier {fakeSupplier.Name} added successfully!\r\n";
            //Act
            string actualMessage = importService.ExposedImportSuppliersFunction();

            //Assert

            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void InvokeAddSupplierRange_WhenAllSuppliersAreValidated()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var mockSupplierService = new Mock<ISupplierService>();
            mockSupplierService.Setup(s => s.SupplierExistsByName(It.IsAny<string>())).Returns(false);
            var fakeJsonService = new Mock<IJsonService>();
            var fakeSupplier = new SuppliersImportModel();
            fakeJsonService.Setup(s => s.DeserializeSuppliers(It.IsAny<string>())).Returns(new[] { fakeSupplier });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(true);
            var importService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                mockSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            //Act
            importService.ExposedImportSuppliersFunction();

            //Assert
            mockSupplierService.Verify(v => v.AddSupplierRange(It.IsAny<IList<ISuppliersImportModel>>()), Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectMessage_WhenMethodReturns()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var mockSupplierService = new Mock<ISupplierService>();
            mockSupplierService.Setup(s => s.SupplierExistsByName(It.IsAny<string>())).Returns(false);
            var fakeJsonService = new Mock<IJsonService>();
            var fakeSupplier1 = new SuppliersImportModel
            {
                Name = "Pesho"
            };
            var fakeSupplier2 = new SuppliersImportModel();
            fakeJsonService.Setup(s => s.DeserializeSuppliers(It.IsAny<string>())).Returns(new[] { fakeSupplier1, fakeSupplier2 });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.SetupSequence(s => s.IsValid(It.IsAny<object>())).Returns(true).Returns(false);
            var importService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                mockSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            string expectedMessage =
                $"Supplier {fakeSupplier1.Name} added successfully!\r\n" +
                "Import rejected. Input is with invalid format.\r\n";
            //Act
            string actualMessage = importService.ExposedImportSuppliersFunction();

            //Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }
    }
}
