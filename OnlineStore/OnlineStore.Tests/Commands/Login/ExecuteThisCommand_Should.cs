using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;
using OnlineStore.DTO.UserModels;
using OnlineStore.DTO.UserModels.Contracts;
using OnlineStore.Logic.Contracts;
using System;

namespace OnlineStore.Tests.Commands.Login
{
    [TestClass]
    public class ExecuteThisCommand_Should
    {
        [TestMethod]
        public void Throw_ArgumentException_When_UserIs_AlreadyLoggedIn()
        {
            // Arrange
            var userServiceStub = new Mock<IUserService>();
            var userSessionMock = new Mock<IUserSession>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            var loginCmd = new LoginCommand(userServiceStub.Object, userSessionMock.Object, writerStub.Object, readerStub.Object, hasherStub.Object);

            userSessionMock
                .Setup(us => us.HasSomeoneLogged())
                .Returns(true);

            Action executingLoginCmd = () => loginCmd.ExecuteThisCommand();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingLoginCmd);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Password_IsIncorrect()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var userSessionMock = new Mock<IUserSession>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            var userStub = new Mock<IUserLoginModel>();

            var loginCmd = new LoginCommand(userServiceMock.Object, userSessionMock.Object, writerStub.Object, readerStub.Object, hasherStub.Object);

            userSessionMock
                .Setup(us => us.HasSomeoneLogged())
                .Returns(false);

            userServiceMock
                .Setup(us => us.GetRegisteredUser(It.IsAny<string>()))
                .Returns(userStub.Object);

            userStub
                .SetupGet(u => u.Password)
                .Returns(It.IsAny<string>());

            hasherStub
                .Setup(h => h.CheckPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            Action executingLoginCmd = () => loginCmd.ExecuteThisCommand();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingLoginCmd);
        }

        [TestMethod]
        public void Invoke_UserSession_Login_When_Password_IsCorrect()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var userSessionMock = new Mock<IUserSession>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            var userStub = new Mock<IUserLoginModel>();

            var loginCmd = new LoginCommand(userServiceMock.Object, userSessionMock.Object, writerStub.Object, readerStub.Object, hasherStub.Object);

            userSessionMock
                .Setup(us => us.HasSomeoneLogged())
                .Returns(false);

            userServiceMock
                .Setup(us => us.GetRegisteredUser(It.IsAny<string>()))
                .Returns(userStub.Object);

            userStub
                .SetupGet(u => u.Password)
                .Returns(It.IsAny<string>());

            hasherStub
                .Setup(h => h.CheckPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            // Act
            loginCmd.ExecuteThisCommand();

            // Assert
            userSessionMock.Verify(us => us.Login(userStub.Object), Times.Once);
        }

        [TestMethod]
        public void Invoke_ReaderRead_Method_Twice()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var userSessionMock = new Mock<IUserSession>();
            var writerStub = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            var userStub = new Mock<IUserLoginModel>();

            var loginCmd = new LoginCommand(userServiceMock.Object, userSessionMock.Object, writerStub.Object, readerMock.Object, hasherStub.Object);

            userSessionMock
                .Setup(us => us.HasSomeoneLogged())
                .Returns(false);

            readerMock
                .Setup(r => r.Read())
                .Verifiable();

            userServiceMock
                .Setup(us => us.GetRegisteredUser(It.IsAny<string>()))
                .Returns(userStub.Object);

            userStub
                .SetupGet(u => u.Password)
                .Returns(It.IsAny<string>());

            hasherStub
                .Setup(h => h.CheckPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            // Act
            loginCmd.ExecuteThisCommand();

            // Assert
            readerMock.Verify(r => r.Read(), Times.Exactly(2));
        }
    }
}
