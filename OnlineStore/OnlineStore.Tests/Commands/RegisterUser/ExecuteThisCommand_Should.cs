using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;
using OnlineStore.DTO.Factory;
using OnlineStore.DTO.UserModels;
using OnlineStore.DTO.UserModels.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;
using System;

namespace OnlineStore.Tests.Commands.RegisterUser
{
    [TestClass]
    public class ExecuteThisCommand_Should
    {
        [TestMethod]
        public void Throw_ArgumentException_When_Someone_Is_LoggedIn()
        {
            // Arrange
            var userSessionStub = new Mock<IUserSession>();
            var userServiceStub = new Mock<IUserService>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            var registerUserCmd = new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, hasherStub.Object);

            userSessionStub.Setup(us => us.HasSomeoneLogged()).Returns(true);

            Action executingRegisterCmd = () => registerUserCmd.ExecuteThisCommand();

            // ACt & Assert
            Assert.ThrowsException<ArgumentException>(executingRegisterCmd);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_EnteredPasswords_AreDifferent()
        {
            // Arrange
            var password = "testpassword";
            var confirmedPassword = "testpassworddifferent";

            var userSessionStub = new Mock<IUserSession>();
            var userServiceStub = new Mock<IUserService>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            var registerUserCmd = new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, hasherStub.Object);

            userSessionStub.Setup(us => us.HasSomeoneLogged()).Returns(false);

            readerStub.SetupSequence(r => r.Read())
                .Returns(It.IsAny<string>())
                .Returns(It.IsAny<string>())
                .Returns(password)
                .Returns(confirmedPassword);

            Action executingRegisterCmd = () => registerUserCmd.ExecuteThisCommand();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingRegisterCmd);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_CreatedUserRegisterModel_IsInvalid()
        {
            // Arrange
            string fakePassword = "testpassword";
            string fakeHash = "someHash";

            var userSessionStub = new Mock<IUserSession>();
            var userServiceStub = new Mock<IUserService>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            var userModelStub = new Mock<IUserRegisterModel>();

            var registerUserCmd = new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, hasherStub.Object);

            userSessionStub.Setup(us => us.HasSomeoneLogged()).Returns(false);

            readerStub.SetupSequence(r => r.Read())
                .Returns(It.IsAny<string>())
                .Returns(It.IsAny<string>())
                .Returns(fakePassword)
                .Returns(fakePassword);

            hasherStub.Setup(h => h.CreatePassword(fakePassword)).Returns(fakeHash);

            dtoFactoryStub.Setup(dtoFac => dtoFac.CreateUserRegisterModel(It.IsAny<string>(), It.IsAny<string>(), fakeHash, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(userModelStub.Object);

            validatorStub.Setup(v => v.IsValid(userModelStub.Object)).Returns(false);

            Action executingRegisterUserCmd = () => registerUserCmd.ExecuteThisCommand();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingRegisterUserCmd);
        }

        [TestMethod]
        public void Get_Eight_ReadValues_From_Reader()
        {
            // Arrange
            string fakePassword = "testpassword";
            string fakeHash = "someHash";

            var userSessionStub = new Mock<IUserSession>();
            var userServiceStub = new Mock<IUserService>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var mockReader = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            var userModelStub = new Mock<IUserRegisterModel>();

            var registerUserCmd = new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, mockReader.Object, hasherStub.Object);

            userSessionStub.Setup(us => us.HasSomeoneLogged()).Returns(false);

            mockReader.SetupSequence(r => r.Read())
                .Returns(It.IsAny<string>())
                .Returns(It.IsAny<string>())
                .Returns(fakePassword)
                .Returns(fakePassword);

            hasherStub.Setup(h => h.CreatePassword(fakePassword)).Returns(fakeHash);

            dtoFactoryStub.Setup(dtoFac => dtoFac.CreateUserRegisterModel(It.IsAny<string>(), It.IsAny<string>(), fakeHash, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(userModelStub.Object);

            validatorStub.Setup(v => v.IsValid(userModelStub.Object)).Returns(true);

            // Act
            registerUserCmd.ExecuteThisCommand();

            // Assert
            mockReader.Verify(r => r.Read(), Times.Exactly(8));
        }

        [TestMethod]
        public void Invoke_Hasher_CreatePasswordMethod_When_EnteredPasswords_AreEqual()
        {
            // Arrange
            string fakePassword = "testpassword";
            string fakeHash = "someHash";

            var userSessionStub = new Mock<IUserSession>();
            var userServiceStub = new Mock<IUserService>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var mockHasher = new Mock<IHasher>();

            var userModelStub = new Mock<IUserRegisterModel>();

            var registerUserCmd = new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, mockHasher.Object);

            userSessionStub.Setup(us => us.HasSomeoneLogged()).Returns(false);

            readerStub.SetupSequence(r => r.Read())
                .Returns(It.IsAny<string>())
                .Returns(It.IsAny<string>())
                .Returns(fakePassword)
                .Returns(fakePassword);

            mockHasher.Setup(h => h.CreatePassword(fakePassword)).Returns(fakeHash);

            dtoFactoryStub.Setup(dtoFac => dtoFac.CreateUserRegisterModel(It.IsAny<string>(), It.IsAny<string>(), fakeHash, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(userModelStub.Object);

            validatorStub.Setup(v => v.IsValid(userModelStub.Object)).Returns(true);

            // Act
            registerUserCmd.ExecuteThisCommand();

            // Assert
            mockHasher.Verify(h => h.CreatePassword(fakePassword), Times.Once);
        }

        [TestMethod]
        public void Invoke_Hasher_ValidatePassword_When_Password_IsEntered()
        {
            // Arrange
            string fakePassword = "testpassword";
            string fakeHash = "someHash";

            var userSessionStub = new Mock<IUserSession>();
            var userServiceStub = new Mock<IUserService>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var mockHasher = new Mock<IHasher>();

            var userModelStub = new Mock<IUserRegisterModel>();

            var registerUserCmd = new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, mockHasher.Object);

            userSessionStub.Setup(us => us.HasSomeoneLogged()).Returns(false);

            readerStub.SetupSequence(r => r.Read())
                .Returns(It.IsAny<string>())
                .Returns(It.IsAny<string>())
                .Returns(fakePassword)
                .Returns(fakePassword);

            mockHasher.Setup(h => h.CreatePassword(fakePassword)).Returns(fakeHash);

            dtoFactoryStub.Setup(dtoFac => dtoFac.CreateUserRegisterModel(It.IsAny<string>(), It.IsAny<string>(), fakeHash, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(userModelStub.Object);

            validatorStub.Setup(v => v.IsValid(userModelStub.Object)).Returns(true);

            // Act
            registerUserCmd.ExecuteThisCommand();

            // Assert
            mockHasher.Verify(h => h.ValidatePassword(fakePassword), Times.Once);
        }

        [TestMethod]
        public void Invoke_DTOFactory_CreateUserRegisterModel_With_CorrectValues()
        {
            // Arrange
            string fakeUsername = "testUser";
            string fakeEmail = "test@Mail";
            string fakePassword = "testpassword";
            string fakeHash = "someHash";
            string fakeFirstName = "";
            string fakeLastName = "";
            string fakeTown = "testTown";
            string fakeAddress = "testAddress";

            var userSessionStub = new Mock<IUserSession>();
            var userServiceStub = new Mock<IUserService>();
            var mockDtoFactory = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            var userModelStub = new Mock<IUserRegisterModel>();

            var registerUserCmd = new RegisterUserCommand(userServiceStub.Object, userSessionStub.Object, mockDtoFactory.Object, validatorStub.Object, writerStub.Object, readerStub.Object, hasherStub.Object);

            userSessionStub.Setup(us => us.HasSomeoneLogged()).Returns(false);

            readerStub.SetupSequence(r => r.Read())
                .Returns(fakeUsername)
                .Returns(fakeEmail)
                .Returns(fakePassword)
                .Returns(fakePassword)
                .Returns(fakeFirstName)
                .Returns(fakeLastName)
                .Returns(fakeTown)
                .Returns(fakeAddress);

            hasherStub.Setup(h => h.CreatePassword(fakePassword)).Returns(fakeHash);

            mockDtoFactory.Setup(dtoFac => dtoFac.CreateUserRegisterModel(fakeUsername, fakeEmail, fakeHash, fakeFirstName, fakeLastName, fakeTown, fakeAddress)).Returns(userModelStub.Object);

            validatorStub.Setup(v => v.IsValid(userModelStub.Object)).Returns(true);

            // Act
            registerUserCmd.ExecuteThisCommand();

            // Assert
            mockDtoFactory.Verify(dtoFac => dtoFac.CreateUserRegisterModel(fakeUsername, fakeEmail, fakeHash, fakeFirstName, fakeLastName, fakeTown, fakeAddress), Times.Once);
        }

        [TestMethod]
        public void Invoke_Service_RegisterUser_With_ValidUserModel()
        {
            // Arrange
            string fakePassword = "testpassword";
            string fakeHash = "someHash";

            var userSessionStub = new Mock<IUserSession>();
            var mockUserService = new Mock<IUserService>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            var userModelStub = new Mock<IUserRegisterModel>();

            var registerUserCmd = new RegisterUserCommand(mockUserService.Object, userSessionStub.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, hasherStub.Object);

            userSessionStub.Setup(us => us.HasSomeoneLogged()).Returns(false);

            readerStub.SetupSequence(r => r.Read())
                .Returns(It.IsAny<string>())
                .Returns(It.IsAny<string>())
                .Returns(fakePassword)
                .Returns(fakePassword);

            hasherStub.Setup(h => h.CreatePassword(fakePassword)).Returns(fakeHash);

            dtoFactoryStub.Setup(dtoFac => dtoFac.CreateUserRegisterModel(It.IsAny<string>(), It.IsAny<string>(), fakeHash, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(userModelStub.Object);

            validatorStub.Setup(v => v.IsValid(userModelStub.Object)).Returns(true);

            // Act
            registerUserCmd.ExecuteThisCommand();

            // Assert
            mockUserService.Verify(us => us.RegisterUser(userModelStub.Object), Times.Once);
        }
    }
}
