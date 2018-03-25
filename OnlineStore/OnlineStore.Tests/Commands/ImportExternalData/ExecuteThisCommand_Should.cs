using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands.AdminCommands;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Tests.Commands.ImportExternalData
{
    [TestClass]
    public class ExecuteThisCommand_Should
    {
        [TestMethod]
        public void RequireLogin_IfNoUserLogged()
        {
            //Arrange
            var fakeUserSession = new Mock<IUserSession>();
            fakeUserSession.Setup(s => s.HasSomeoneLogged()).Returns(false);
            var fakeImportService = new Mock<IImportService>();
            var importCommand = new ImportExternalDataCommand(fakeImportService.Object, fakeUserSession.Object);
            var expectedMessage = "Login First!";

            //Act
            var actualMessage = importCommand.ExecuteThisCommand();

            //Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void RequireAdminRights_IfUserIsLogged()
        {
            //Arrange
            var fakeUserSession = new Mock<IUserSession>();
            fakeUserSession.Setup(s => s.HasSomeoneLogged()).Returns(true);
            fakeUserSession.Setup(s => s.HasAdminRights()).Returns(false);

            var fakeImportService = new Mock<IImportService>();
            var importCommand = new ImportExternalDataCommand(fakeImportService.Object, fakeUserSession.Object);
            var expectedMessage = "User must be admin or moderator in order to import data!";

            //Act
            var actualMessage = importCommand.ExecuteThisCommand();

            //Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void InvokeImportMethod_IfUserIsLoggedAndIsAdmin()
        {
            //Arrange
            var fakeUserSession = new Mock<IUserSession>();
            fakeUserSession.Setup(s => s.HasSomeoneLogged()).Returns(true);
            fakeUserSession.Setup(s => s.HasAdminRights()).Returns(true);

            var fakeImportService = new Mock<IImportService>();
            var importCommand = new ImportExternalDataCommand(fakeImportService.Object, fakeUserSession.Object);

            //Act
            importCommand.ExecuteThisCommand();
            //Assert
            fakeImportService.Verify(v => v.Import(), Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectResult_IfUserIsLoggedAndIsAdmin()
        {
            //Arrange
            var fakeUserSession = new Mock<IUserSession>();
            fakeUserSession.Setup(s => s.HasSomeoneLogged()).Returns(true);
            fakeUserSession.Setup(s => s.HasAdminRights()).Returns(true);

            var expectedResult = "test";

            var fakeImportService = new Mock<IImportService>();
            fakeImportService.Setup(s => s.Import()).Returns(expectedResult);
            var importCommand = new ImportExternalDataCommand(fakeImportService.Object, fakeUserSession.Object);
            
            //Act
            var actualResult =  importCommand.ExecuteThisCommand();
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
