using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands.AdminCommands;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;

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
            var fakeProductService = new Mock<IProductService>();

            //Act && Assert
            Assert.IsInstanceOfType(new AddProductToProductsCommand(fakeProductService.Object, fakeUserSession.Object, fakeReader.Object, fakeWriter.Object), typeof(AddProductToProductsCommand));
        }

        [TestMethod]
        public void Throw_WhenReaderIsNull()
        {
            //Arrange
            var fakeWriter = new Mock<IWriter>();
            var fakeUserSession = new Mock<IUserSession>();
            var fakeProductService = new Mock<IProductService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AddProductToProductsCommand(fakeProductService.Object, fakeUserSession.Object, null, fakeWriter.Object));

        }
        [TestMethod]
        public void Throw_WhenWriterIsNull()
        {
            //Arrange
            var fakeReader = new Mock<IReader>();
            var fakeUserSession = new Mock<IUserSession>();
            var fakeProductService = new Mock<IProductService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AddProductToProductsCommand(fakeProductService.Object, fakeUserSession.Object, fakeReader.Object, null));
        }
        [TestMethod]
        public void Throw_WhenUserSessionIsNull()
        {
            //Arrange
            var fakeReader = new Mock<IReader>();
            var fakeWriter = new Mock<IWriter>();
            var fakeProductService = new Mock<IProductService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AddProductToProductsCommand(fakeProductService.Object, null, fakeReader.Object, fakeWriter.Object));
        }
        [TestMethod]
        public void Throw_WhenProductServiceIsNull()
        {
            //Arrange
            var fakeReader = new Mock<IReader>();
            var fakeWriter = new Mock<IWriter>();
            var fakeUserSession = new Mock<IUserSession>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AddProductToProductsCommand(null, fakeUserSession.Object, fakeReader.Object, fakeWriter.Object));
        }
    }
}
