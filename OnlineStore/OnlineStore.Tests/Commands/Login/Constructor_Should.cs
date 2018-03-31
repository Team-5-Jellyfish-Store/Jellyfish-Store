using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;
using System;

namespace OnlineStore.Tests.Commands.Login
{
    [TestClass]
    public class Constructor_Should
    {
        private Mock<IUserService> userServiceStub;
        private Mock<IUserSession> userSessionStub;
        private Mock<IWriter> writerStub;
        private Mock<IReader> readerStub;
        private Mock<IHasher> hasherStub;

        [TestInitialize]
        public void Initialize()
        {
            userServiceStub = new Mock<IUserService>();
            userSessionStub = new Mock<IUserSession>();
            writerStub = new Mock<IWriter>();
            readerStub = new Mock<IReader>();
            hasherStub = new Mock<IHasher>();
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_UserService_IsNull()
        {
            // Arrange
            Action creatingLoginCmd = () => new LoginCommand(null, userSessionStub.Object, writerStub.Object, readerStub.Object, hasherStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingLoginCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_UserSession_IsNull()
        {
            // Arrange
            Action creatingLoginCmd = () => new LoginCommand(userServiceStub.Object, null, writerStub.Object, readerStub.Object, hasherStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingLoginCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Writer_IsNull()
        {
            // Arrange
            Action creatingLoginCmd = () => new LoginCommand(userServiceStub.Object, userSessionStub.Object, null, readerStub.Object, hasherStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingLoginCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Reader_IsNull()
        {
            // Arrange
            Action creatingLoginCmd = () => new LoginCommand(userServiceStub.Object, userSessionStub.Object, writerStub.Object, null, hasherStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingLoginCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Hasher_IsNull()
        {
            // Arrange
            Action creatingLoginCmd = () => new LoginCommand(userServiceStub.Object, userSessionStub.Object, writerStub.Object, readerStub.Object, null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingLoginCmd);
        }
    }
}
