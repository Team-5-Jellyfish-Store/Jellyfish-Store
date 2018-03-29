using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;

namespace OnlineStore.Tests.Services.ImportService
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnInstance_WhenProvidedCorrectParameters()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            var fakeJsonService = new Mock<IJsonService>();

            //Act && Assert
            Assert.IsInstanceOfType(new Logic.Services.ImportService( fakeProductService.Object, fakeCourierService.Object, fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object), typeof(IImportService));
        }

        [TestMethod]
        public void Throw_WhenProductServiceIsNull()
        {
            //Arrange
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            var fakeJsonService = new Mock<IJsonService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>( () => new Logic.Services.ImportService(null, fakeCourierService.Object, fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object));
        }

        [TestMethod]
        public void Throw_WhenCourierServiceIsNull()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            var fakeJsonService = new Mock<IJsonService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Logic.Services.ImportService(fakeProductService.Object, null, fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object));
        }

        [TestMethod]
        public void Throw_WhenSupplierServiceIsNull()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            var fakeJsonService = new Mock<IJsonService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Logic.Services.ImportService(fakeProductService.Object, fakeCourierService.Object, null, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object));
        }

        [TestMethod]
        public void Throw_WhenFileReaderIsNull()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeValidator = new Mock<IValidator>();
            var fakeJsonService = new Mock<IJsonService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Logic.Services.ImportService(fakeProductService.Object, fakeCourierService.Object, fakeSupplierService.Object, null, fakeValidator.Object, fakeJsonService.Object));
        }

        [TestMethod]
        public void Throw_WhenFakeValidatorIsNull()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeFileReader = new Mock<IFileReader>();
            var fakeJsonService = new Mock<IJsonService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Logic.Services.ImportService(fakeProductService.Object, fakeCourierService.Object, fakeSupplierService.Object, fakeFileReader.Object, null, fakeJsonService.Object));
        }

        [TestMethod]
        public void Throw_WhenFakeJsonServiceIsNull()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Logic.Services.ImportService(fakeProductService.Object, fakeCourierService.Object, fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object , null));
        }
    }
}
