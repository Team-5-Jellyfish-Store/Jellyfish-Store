﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands.AdminCommands;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Tests.Commands.AddProductToProducts
{
    [TestClass]
    public class ExecuteThisCommand_Should
    {
        [TestMethod]
        public void RequireLogin_IfNoUserLogged()
        {
            //Arrange
            var fakeReader = new Mock<IReader>();
            var fakeWriter = new Mock<IWriter>();
            var fakeUserSession = new Mock<IUserSession>();
            var fakeProductService = new Mock<IProductService>();
            fakeUserSession.Setup(s => s.HasSomeoneLogged()).Returns(false);
            var addProductCommand = new AddProductToProductsCommand(fakeProductService.Object, fakeUserSession.Object, fakeReader.Object, fakeWriter.Object);
            var expectedMessage = "Login first!";

            //Act
            var actualMessage = addProductCommand.ExecuteThisCommand();

            //Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void RequireAdminRights_IfUserIsLogged()
        {
            //Arrange
            var fakeReader = new Mock<IReader>();
            var fakeWriter = new Mock<IWriter>();
            var fakeUserSession = new Mock<IUserSession>();
            var fakeProductService = new Mock<IProductService>();
            fakeUserSession.Setup(s => s.HasSomeoneLogged()).Returns(true);
            fakeUserSession.Setup(s => s.HasAdminRights()).Returns(false);
            var addProductCommand = new AddProductToProductsCommand(fakeProductService.Object, fakeUserSession.Object, fakeReader.Object, fakeWriter.Object);
            var expectedMessage = "User is neither admin nor moderator and cannot add products!";

            //Act
            var actualMessage = addProductCommand.ExecuteThisCommand();

            //Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void InvokeCreateProductMethod_IfUserIsLoggedAndIsAdmin()
        {
            //Arrange
            var fakeReader = new Mock<IReader>();
            fakeReader.SetupSequence(s => s.Read())
                .Returns("test")
                .Returns("5.50")
                .Returns("5")
                .Returns("test")
                .Returns("test");

            var fakeWriter = new Mock<IWriter>();
            var fakeUserSession = new Mock<IUserSession>();

            var fakeProductService = new Mock<IProductService>();
            fakeUserSession.Setup(s => s.HasSomeoneLogged()).Returns(true);
            fakeUserSession.Setup(s => s.HasAdminRights()).Returns(true);

            var addProductCommand = new AddProductToProductsCommand(fakeProductService.Object, fakeUserSession.Object, fakeReader.Object, fakeWriter.Object);

            //Act
            addProductCommand.ExecuteThisCommand();
            //Assert
            fakeProductService.Verify(v => v.CreateProduct(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectResult_IfUserIsLoggedAndIsAdmin()
        {
            //Arrange
            var fakeReader = new Mock<IReader>();
            fakeReader.SetupSequence(s => s.Read())
                .Returns("test")
                .Returns("5.50")
                .Returns("5")
                .Returns("test")
                .Returns("test");

            var fakeWriter = new Mock<IWriter>();
            var fakeUserSession = new Mock<IUserSession>();

            var fakeProductService = new Mock<IProductService>();
            fakeUserSession.Setup(s => s.HasSomeoneLogged()).Returns(true);
            fakeUserSession.Setup(s => s.HasAdminRights()).Returns(true);

            var addProductCommand = new AddProductToProductsCommand(fakeProductService.Object, fakeUserSession.Object, fakeReader.Object, fakeWriter.Object);
            var expectedResult = $"Product test added successfully!";
            //Act
            var actualResult = addProductCommand.ExecuteThisCommand();
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
