using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands.AdminCommands;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;
using OnlineStore.DTO.Factory;

namespace OnlineStore.Tests.Commands.AddProductToProducts
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnInstance_WhenProvidedValidParameters()
        {
            //Arrange
            var fakeReader = new Mock<IReader>();
            var fakeWriter = new Mock<IWriter>();
            var fakeUserSession = new Mock<IUserSession>();
            var fakeDtoFactory = new Mock<IDataTransferObjectFactory>();
            var fakeProductService = new Mock<IProductService>();
            var fakeValidator = new Mock<IValidator>();

            //Act && Assert
            Assert.IsInstanceOfType(new AddProductToProductsCommand(fakeProductService.Object, fakeUserSession.Object, fakeDtoFactory.Object, fakeReader.Object, fakeWriter.Object, fakeValidator.Object), typeof(AddProductToProductsCommand));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenReaderIsNull()
        {
            //Arrange
            var fakeWriter = new Mock<IWriter>();
            var fakeUserSession = new Mock<IUserSession>();
            var fakeProductService = new Mock<IProductService>();
            var fakeDtoFactory = new Mock<IDataTransferObjectFactory>();
            var fakeValidator = new Mock<IValidator>();


            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AddProductToProductsCommand(fakeProductService.Object, fakeUserSession.Object, fakeDtoFactory.Object, null, fakeWriter.Object, fakeValidator.Object));

        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenWriterIsNull()
        {
            //Arrange
            var fakeReader = new Mock<IReader>();
            var fakeUserSession = new Mock<IUserSession>();
            var fakeProductService = new Mock<IProductService>();
            var fakeDtoFactory = new Mock<IDataTransferObjectFactory>();
            var fakeValidator = new Mock<IValidator>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AddProductToProductsCommand(fakeProductService.Object, fakeUserSession.Object, fakeDtoFactory.Object, fakeReader.Object, null, fakeValidator.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenUserSessionIsNull()
        {
            //Arrange
            var fakeReader = new Mock<IReader>();
            var fakeWriter = new Mock<IWriter>();
            var fakeProductService = new Mock<IProductService>();
            var fakeDtoFactory = new Mock<IDataTransferObjectFactory>();
            var fakeValidator = new Mock<IValidator>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AddProductToProductsCommand(fakeProductService.Object, null, fakeDtoFactory.Object, fakeReader.Object, fakeWriter.Object, fakeValidator.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenProductServiceIsNull()
        {
            //Arrange
            var fakeReader = new Mock<IReader>();
            var fakeWriter = new Mock<IWriter>();
            var fakeUserSession = new Mock<IUserSession>();
            var fakeDtoFactory = new Mock<IDataTransferObjectFactory>();
            var fakeValidator = new Mock<IValidator>();


            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AddProductToProductsCommand(null, fakeUserSession.Object, fakeDtoFactory.Object, fakeReader.Object, fakeWriter.Object, fakeValidator.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenValidatorIsNull()
        {
            //Arrange
            var fakeReader = new Mock<IReader>();
            var fakeWriter = new Mock<IWriter>();
            var fakeUserSession = new Mock<IUserSession>();
            var fakeDtoFactory = new Mock<IDataTransferObjectFactory>();
            var fakeService = new Mock<IProductService>();


            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AddProductToProductsCommand(fakeService.Object, fakeUserSession.Object, fakeDtoFactory.Object, fakeReader.Object, fakeWriter.Object, null));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenDTOFactoryIsNull()
        {
            //Arrange
            var fakeReader = new Mock<IReader>();
            var fakeWriter = new Mock<IWriter>();
            var fakeUserSession = new Mock<IUserSession>();
            var fakeService = new Mock<IProductService>();
            var fakeValidator = new Mock<IValidator>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AddProductToProductsCommand(fakeService.Object, fakeUserSession.Object, null, fakeReader.Object, fakeWriter.Object, fakeValidator.Object));
        }
    }
}
