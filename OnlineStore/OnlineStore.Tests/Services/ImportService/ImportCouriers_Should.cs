using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Contracts;
using OnlineStore.DTO.CourierModels;
using OnlineStore.DTO.SupplierModels;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;
using OnlineStore.Tests.Mocks;

namespace OnlineStore.Tests.Services.ImportService
{
    [TestClass]
    public class ImportCouriers_Should
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
            importService.ExposedImportCouriersFunction();

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
            importService.ExposedImportCouriersFunction();

            //Assert
            mockJsonService.Verify(v => v.DeserializeCouriers(It.IsAny<string>()), Times.Exactly(1));
        }

        [TestMethod]
        public void InvokeValidator_ToValidateObject()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeJsonService = new Mock<IJsonService>();
            fakeJsonService.Setup(s => s.DeserializeCouriers(It.IsAny<string>())).Returns(new[] { new CourierImportModel() });
            var fakeFileReader = new Mock<IFileReader>();
            var mockValidator = new Mock<IValidator>();
            var importService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, mockValidator.Object, fakeJsonService.Object);
            //Act
            importService.ExposedImportCouriersFunction();

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
            fakeJsonService.Setup(s => s.DeserializeCouriers(It.IsAny<string>())).Returns(new[] { new CourierImportModel() });
            var fakeFileReader = new Mock<IFileReader>();
            var mockValidator = new Mock<IValidator>();
            mockValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(false);
            var importService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, mockValidator.Object, fakeJsonService.Object);
            string expectedMessage = "Import rejected. Input is with invalid format.\r\n";
            //Act
            string actualMessage = importService.ExposedImportCouriersFunction();

            //Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void InvokeCourierExistsByName_WhenModelIsValid()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var mockCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeJsonService = new Mock<IJsonService>();
            fakeJsonService.Setup(s => s.DeserializeCouriers(It.IsAny<string>())).Returns(new[] { new CourierImportModel() });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(true);
            var importService = new MockImportService(fakeProductService.Object, mockCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            //Act
            importService.ExposedImportCouriersFunction();

            //Assert
            mockCourierService.Verify(v => v.CourierExistsByName(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
        }

        [TestMethod]
        public void ReturnsCorrectMessage_WhenModelIsValidAndCourierExists()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var mockCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            mockCourierService.Setup(s => s.CourierExistsByName(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var fakeJsonService = new Mock<IJsonService>();
            var fakeCourier = new CourierImportModel()
            {
                FirstName = "Pesho",
                LastName = "Goshov"
            };
            fakeJsonService.Setup(s => s.DeserializeCouriers(It.IsAny<string>())).Returns(new[] { fakeCourier });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(true);
            var importService = new MockImportService(fakeProductService.Object, mockCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            string expectedMessage = $"Courier {fakeCourier.FirstName} {fakeCourier.LastName} already exists!\r\n";
            //Act
            string actualMessage = importService.ExposedImportCouriersFunction();

            //Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void AddsTheCourierToListOfValidCouriers_WhenModelIsValidAndCourierDoesNotExist()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var mockCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            mockCourierService.Setup(s => s.CourierExistsByName(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var fakeJsonService = new Mock<IJsonService>();
            var fakeCourier = new CourierImportModel()
            {
                FirstName = "Pesho",
                LastName = "Goshov"
            };
            fakeJsonService.Setup(s => s.DeserializeCouriers(It.IsAny<string>())).Returns(new[] { fakeCourier });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(true);
            var importService = new MockImportService(fakeProductService.Object, mockCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);

            //Act
            importService.ExposedImportCouriersFunction();

            //Assert
            Assert.IsTrue(importService.ExposedValidCouriers.Any(a => a.FirstName == fakeCourier.FirstName && a.LastName == fakeCourier.LastName));
        }

        [TestMethod]
        public void ReturnsValidMessage_WhenCourierIsAdded()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var mockCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            mockCourierService.Setup(s => s.CourierExistsByName(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var fakeJsonService = new Mock<IJsonService>();
            var fakeCourier = new CourierImportModel()
            {
                FirstName = "Pesho",
                LastName = "Goshov"
            };
            fakeJsonService.Setup(s => s.DeserializeCouriers(It.IsAny<string>())).Returns(new[] { fakeCourier });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(true);
            var importService = new MockImportService(fakeProductService.Object, mockCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            string expectedMessage = $"Courier {fakeCourier.FirstName} {fakeCourier.LastName} added successfully!\r\n";
            //Act
            string actualMessage = importService.ExposedImportCouriersFunction();

            //Assert

            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void InvokeAddCourierRange_WhenAllCouriersAreValidated()
        {
            var fakeProductService = new Mock<IProductService>();
            var mockCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            mockCourierService.Setup(s => s.CourierExistsByName(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var fakeJsonService = new Mock<IJsonService>();
            fakeJsonService.Setup(s => s.DeserializeCouriers(It.IsAny<string>())).Returns(new[] { new CourierImportModel() });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(true);
            var importService = new MockImportService(fakeProductService.Object, mockCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            //Act
            importService.ExposedImportCouriersFunction();

            //Assert
            mockCourierService.Verify(v => v.AddCourierRange(It.IsAny<IList<ICourierImportModel>>()), Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectMessage_WhenMethodReturns()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var mockCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            mockCourierService.Setup(s => s.CourierExistsByName(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var fakeJsonService = new Mock<IJsonService>();
            var fakeCourier1 = new CourierImportModel()
            {
                FirstName = "Pesho",
                LastName = "Goshov"
            };
            var fakeCourier2 = new CourierImportModel();
            fakeJsonService.Setup(s => s.DeserializeCouriers(It.IsAny<string>())).Returns(new[] { fakeCourier1, fakeCourier2 });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.SetupSequence(s => s.IsValid(It.IsAny<object>())).Returns(true).Returns(false);
            var importService = new MockImportService(fakeProductService.Object, mockCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            string expectedMessage =
                $"Courier {fakeCourier1.FirstName} {fakeCourier1.LastName} added successfully!\r\n" +
                "Import rejected. Input is with invalid format.\r\n";
            //Act
            string actualMessage = importService.ExposedImportCouriersFunction();

            //Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }
    }
}
