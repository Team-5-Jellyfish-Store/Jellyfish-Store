using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands.AdminCommands;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Tests.Commands.RemoveProductFromProducts
{
    [TestClass]
    public class ExecuteThisCommand_Should
    {
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
            var removeProductCommand = new RemoveProductFromProductsCommand(fakeProductService.Object, fakeUserSession.Object, fakeReader.Object, fakeWriter.Object);
            var expectedMessage = "User is neither admin nor moderator and cannot add products!";

            //Act
            var actualMessage = removeProductCommand.ExecuteThisCommand();

            //Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void InvokeRemoveProductByNameMethod_IfUserIsLoggedAndIsAdmin()
        {
            //Arrange
            var fakeReader = new Mock<IReader>();
            fakeReader.Setup(s => s.Read()).Returns("test");
            var fakeWriter = new Mock<IWriter>();
            var fakeUserSession = new Mock<IUserSession>();

            var fakeProductService = new Mock<IProductService>();
            fakeUserSession.Setup(s => s.HasSomeoneLogged()).Returns(true);
            fakeUserSession.Setup(s => s.HasAdminRights()).Returns(true);

            var removeProductCommand = new RemoveProductFromProductsCommand(fakeProductService.Object, fakeUserSession.Object, fakeReader.Object, fakeWriter.Object);

            //Act
            removeProductCommand.ExecuteThisCommand();
            //Assert
            fakeProductService.Verify(v => v.RemoveProductByName(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectResult_IfUserIsLoggedAndIsAdmin()
        {
            //Arrange
            var fakeReader = new Mock<IReader>();
            fakeReader.Setup(s => s.Read()).Returns("test");

            var fakeWriter = new Mock<IWriter>();
            var fakeUserSession = new Mock<IUserSession>();

            var fakeProductService = new Mock<IProductService>();
            fakeUserSession.Setup(s => s.HasSomeoneLogged()).Returns(true);
            fakeUserSession.Setup(s => s.HasAdminRights()).Returns(true);

            var removeProductCommand = new RemoveProductFromProductsCommand(fakeProductService.Object, fakeUserSession.Object, fakeReader.Object, fakeWriter.Object);
            var expectedResult = "Product test removed successfully!";
            //Act
            var actualResult = removeProductCommand.ExecuteThisCommand();
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
