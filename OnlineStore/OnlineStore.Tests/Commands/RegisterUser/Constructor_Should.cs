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
        [TestMethod]
        public void Throw_ArgumentNullException_When_UserService_IsNull()
        {
            var userSessionStub = new Mock<IUserSession>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            Action creatingRegisterUserCmd = () => new RegisterUserCommand(null, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, hasherStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingRegisterUserCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_UserSession_IsNull()
        {
            var userServiceStub = new Mock<IUserService>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            Action creatingRegisterUserCmd = () => new RegisterUserCommand(userServiceStub.Object, null, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, hasherStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingRegisterUserCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_DTOFactory_IsNull()
        {
            var userServiceStub = new Mock<IUserService>();
            var userSessionStub = new Mock<IUserSession>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            Action creatingRegisterUserCmd = () => new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, null, validatorStub.Object, writerStub.Object, readerStub.Object, hasherStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingRegisterUserCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Validator_IsNull()
        {
            var userServiceStub = new Mock<IUserService>();
            var userSessionStub = new Mock<IUserSession>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            Action creatingRegisterUserCmd = () => new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, null, writerStub.Object, readerStub.Object, hasherStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingRegisterUserCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Writer_IsNull()
        {
            var userServiceStub = new Mock<IUserService>();
            var userSessionStub = new Mock<IUserSession>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var readerStub = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            Action creatingRegisterUserCmd = () => new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, null, readerStub.Object, hasherStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingRegisterUserCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Reader_IsNull()
        {
            var userServiceStub = new Mock<IUserService>();
            var userSessionStub = new Mock<IUserSession>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var hasherStub = new Mock<IHasher>();

            Action creatingRegisterUserCmd = () => new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, null, hasherStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingRegisterUserCmd);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Hasher_IsNull()
        {
            var userServiceStub = new Mock<IUserService>();
            var userSessionStub = new Mock<IUserSession>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();

            Action creatingRegisterUserCmd = () => new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingRegisterUserCmd);
        }
    }
}
