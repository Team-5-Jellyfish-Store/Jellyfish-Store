using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;
using OnlineStore.DTO.Factory;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;
using System;

namespace OnlineStore.Tests.Commands.RegisterUser
{
    [TestClass]
    public class Constructor_Should
    {
        private Mock<IUserService> userServiceStub;
        private Mock<IUserSession> userSessionStub;
        private Mock<IDataTransferObjectFactory> dtoFactoryStub;
        private Mock<IValidator> validatorStub;
        private Mock<IWriter> writerStub;
        private Mock<IReader> readerStub;
        private Mock<IHasher> hasherStub;

        [TestInitialize]
        public void Initialize()
        {
            userServiceStub = new Mock<IUserService>();
            userSessionStub = new Mock<IUserSession>();
            dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            validatorStub = new Mock<IValidator>();
            writerStub = new Mock<IWriter>();
            readerStub = new Mock<IReader>();
            hasherStub = new Mock<IHasher>();
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_UserService_IsNull()
        {
            //Arrange
            Action creatingRegisterUserCmd = () => new RegisterUserCommand(null, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, hasherStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingRegisterUserCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_UserSession_IsNull()
        {
            // Arrange
            Action creatingRegisterUserCmd = () => new RegisterUserCommand(userServiceStub.Object, null, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, hasherStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingRegisterUserCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_DTOFactory_IsNull()
        {
            // Arrange
            Action creatingRegisterUserCmd = () => new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, null, validatorStub.Object, writerStub.Object, readerStub.Object, hasherStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingRegisterUserCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Validator_IsNull()
        {
            // Arrange
            Action creatingRegisterUserCmd = () => new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, null, writerStub.Object, readerStub.Object, hasherStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingRegisterUserCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Writer_IsNull()
        {
            // Arrange
            Action creatingRegisterUserCmd = () => new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, null, readerStub.Object, hasherStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingRegisterUserCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Reader_IsNull()
        {
            // Arrange
            Action creatingRegisterUserCmd = () => new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, null, hasherStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingRegisterUserCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Hasher_IsNull()
        {
            // Arrange
            Action creatingRegisterUserCmd = () => new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingRegisterUserCmd);
        }
    }
}
