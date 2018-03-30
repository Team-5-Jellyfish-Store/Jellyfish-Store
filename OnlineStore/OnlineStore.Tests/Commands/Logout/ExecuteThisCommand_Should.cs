using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;
using System;

namespace OnlineStore.Tests.Commands.Logout
{
    [TestClass]
    public class ExecuteThisCommand_Should
    {
        [TestMethod]
        public void Throw_ArgumentException_When_NoUser_HasLoggedIn()
        {
            // Arrange
            var userSessionStub = new Mock<IUserSession>();
            var logoutCmd = new LogoutCommand(userSessionStub.Object);

            userSessionStub
                .Setup(us => us.HasSomeoneLogged())
                .Returns(false);

            Action executingLogoutCmd = () => logoutCmd.ExecuteThisCommand();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingLogoutCmd);
        }

        [TestMethod]
        public void Invoke_UserSession_GetLoggetUsername_IfSomeOne_IsLoggedIn()
        {
            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            var logoutCmd = new LogoutCommand(mockUserSession.Object);

            mockUserSession
                .Setup(us => us.HasSomeoneLogged())
                .Returns(true);

            // Act
            logoutCmd.ExecuteThisCommand();

            // Assert
            mockUserSession.Verify(us => us.GetLoggedUserName(), Times.Once);
        }

        [TestMethod]
        public void Invoke_UserSession_LogoutMethod_IfSomeOne_IsLoggedIn()
        {
            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            var logoutCmd = new LogoutCommand(mockUserSession.Object);

            mockUserSession
                .Setup(us => us.HasSomeoneLogged())
                .Returns(true);

            // Act
            logoutCmd.ExecuteThisCommand();

            // Assert
            mockUserSession.Verify(us => us.Logout(), Times.Once);
        }
    }
}
