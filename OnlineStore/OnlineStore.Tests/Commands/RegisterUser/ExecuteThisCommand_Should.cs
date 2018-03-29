﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var userSessionMock = new Mock<IUserSession>();
            var userServiceStub = new Mock<IUserService>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerStub = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            var registerUserCmd = new RegisterUserCommand(userServiceStub.Object, userSessionMock.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerStub.Object, hasherStub.Object);

            userSessionMock.Setup(us => us.HasSomeoneLogged()).Returns(true);

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

            var userSessionMock = new Mock<IUserSession>();
            var userServiceStub = new Mock<IUserService>();
            var dtoFactoryStub = new Mock<IDataTransferObjectFactory>();
            var validatorStub = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var hasherStub = new Mock<IHasher>();

            var registerUserCmd = new RegisterUserCommand(userServiceStub.Object, userSessionMock.Object, dtoFactoryStub.Object, validatorStub.Object, writerStub.Object, readerMock.Object, hasherStub.Object);

            userSessionMock.Setup(us => us.HasSomeoneLogged()).Returns(false);

            readerMock.SetupSequence(r => r.Read())
                .Returns(It.IsAny<string>())
                .Returns(It.IsAny<string>())
                .Returns(password)
                .Returns(confirmedPassword);

            Action executingRegisterCmd = () => registerUserCmd.ExecuteThisCommand();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingRegisterCmd);
        }

        [TestMethod]
        public void Get_Eight_ReadValues_From_Reader()
        {
            // Arrange
            string password = "testpassword";
            string hashMock = "someHash";

            var userSessionMock = new Mock<IUserSession>();
            var userServiceStub = new Mock<IUserService>();
            var dtoFactoryMock = new Mock<IDataTransferObjectFactory>();
            var validatorMock = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var hasherMock = new Mock<IHasher>();

            var userModelStub = new Mock<IUserRegisterModel>();

            var registerUserCmd = new RegisterUserCommand(userServiceStub.Object, userSessionMock.Object, dtoFactoryMock.Object, validatorMock.Object, writerStub.Object, readerMock.Object, hasherMock.Object);

            hasherMock.Setup(h => h.CreatePassword(password)).Returns(hashMock);

            userSessionMock.Setup(us => us.HasSomeoneLogged()).Returns(false);

            dtoFactoryMock.Setup(dtoFac => dtoFac.CreateUserRegisterModel(It.IsAny<string>(), It.IsAny<string>(), hashMock, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(userModelStub.Object);

            validatorMock.Setup(v => v.IsValid(userModelStub.Object)).Returns(true);

            readerMock.SetupSequence(r => r.Read())
                .Returns(It.IsAny<string>())
                .Returns(It.IsAny<string>())
                .Returns(password)
                .Returns(password);

            // Act
            registerUserCmd.ExecuteThisCommand();

            // Assert
            readerMock.Verify(r => r.Read(), Times.Exactly(8));
        }

        [TestMethod]
        public void Invoke_Hasher_CreatePasswordMethod_When_EnteredPasswords_AreEqual()
        {
            // Arrange
            string password = "testpassword";
            string hashMock = "someHash";

            var userSessionMock = new Mock<IUserSession>();
            var userServiceStub = new Mock<IUserService>();
            var dtoFactoryMock = new Mock<IDataTransferObjectFactory>();
            var validatorMock = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var hasherMock = new Mock<IHasher>();

            var userModelStub = new Mock<IUserRegisterModel>();

            var registerUserCmd = new RegisterUserCommand(userServiceStub.Object, userSessionMock.Object, dtoFactoryMock.Object, validatorMock.Object, writerStub.Object, readerMock.Object, hasherMock.Object);

            hasherMock.Setup(h => h.CreatePassword(password)).Returns(hashMock);

            userSessionMock.Setup(us => us.HasSomeoneLogged()).Returns(false);

            dtoFactoryMock.Setup(dtoFac => dtoFac.CreateUserRegisterModel(It.IsAny<string>(), It.IsAny<string>(), hashMock, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(userModelStub.Object);

            validatorMock.Setup(v => v.IsValid(userModelStub.Object)).Returns(true);

            readerMock.SetupSequence(r => r.Read())
                .Returns(It.IsAny<string>())
                .Returns(It.IsAny<string>())
                .Returns(password)
                .Returns(password);

            // Act
            registerUserCmd.ExecuteThisCommand();

            // Assert
            hasherMock.Verify(h => h.CreatePassword(password), Times.Once);
        }

        [TestMethod]
        public void Invoke_Hasher_ValidatePassword_When_Password_IsEntered()
        {
            // Arrange
            string password = "testpassword";
            string hashMock = "someHash";

            var userSessionMock = new Mock<IUserSession>();
            var userServiceStub = new Mock<IUserService>();
            var dtoFactoryMock = new Mock<IDataTransferObjectFactory>();
            var validatorMock = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var hasherMock = new Mock<IHasher>();

            var userModelStub = new Mock<IUserRegisterModel>();

            var registerUserCmd = new RegisterUserCommand(userServiceStub.Object, userSessionMock.Object, dtoFactoryMock.Object, validatorMock.Object, writerStub.Object, readerMock.Object, hasherMock.Object);

            hasherMock.Setup(h => h.CreatePassword(password)).Returns(hashMock);

            userSessionMock.Setup(us => us.HasSomeoneLogged()).Returns(false);

            dtoFactoryMock.Setup(dtoFac => dtoFac.CreateUserRegisterModel(It.IsAny<string>(), It.IsAny<string>(), hashMock, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(userModelStub.Object);

            validatorMock.Setup(v => v.IsValid(userModelStub.Object)).Returns(true);

            readerMock.SetupSequence(r => r.Read())
                .Returns(It.IsAny<string>())
                .Returns(It.IsAny<string>())
                .Returns(password)
                .Returns(password);

            // Act
            registerUserCmd.ExecuteThisCommand();

            // Assert
            hasherMock.Verify(h => h.ValidatePassword(password), Times.Once);
        }

        [TestMethod]
        public void Invoke_CreateUserRegisterModel_With_CorrectValues()
        {
            // Arrange
            string username = "testUser";
            string email = "test@Mail";
            string password = "testpassword";
            string hashMock = "someHash";
            string firstName = "";
            string lastName = "";
            string town = "testTown";
            string address = "testAddress";

            var userSessionMock = new Mock<IUserSession>();
            var userServiceStub = new Mock<IUserService>();
            var dtoFactoryMock = new Mock<IDataTransferObjectFactory>();
            var validatorMock = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var hasherMock = new Mock<IHasher>();

            var userModelStub = new Mock<IUserRegisterModel>();

            var registerUserCmd = new RegisterUserCommand(userServiceStub.Object, userSessionMock.Object, dtoFactoryMock.Object, validatorMock.Object, writerStub.Object, readerMock.Object, hasherMock.Object);

            hasherMock.Setup(h => h.CreatePassword(password)).Returns(hashMock);

            userSessionMock.Setup(us => us.HasSomeoneLogged()).Returns(false);

            dtoFactoryMock.Setup(dtoFac => dtoFac.CreateUserRegisterModel(username, email, hashMock, firstName, lastName, town, address)).Returns(userModelStub.Object);

            validatorMock.Setup(v => v.IsValid(userModelStub.Object)).Returns(true);

            readerMock.SetupSequence(r => r.Read())
                .Returns(username)
                .Returns(email)
                .Returns(password)
                .Returns(password)
                .Returns(firstName)
                .Returns(lastName)
                .Returns(town)
                .Returns(address);

            // Act
            registerUserCmd.ExecuteThisCommand();

            // Assert
            dtoFactoryMock.Verify(dtoFac => dtoFac.CreateUserRegisterModel(username, email, hashMock, firstName, lastName, town, address), Times.Once);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_CreatedUserRegisterModel_IsInvalid()
        {
            // Arrange
            string password = "testpassword";
            string hashMock = "someHash";

            var userSessionMock = new Mock<IUserSession>();
            var userServiceStub = new Mock<IUserService>();
            var dtoFactoryMock = new Mock<IDataTransferObjectFactory>();
            var validatorMock = new Mock<IValidator>();
            var writerStub = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var hasherMock = new Mock<IHasher>();

            var userModelStub = new Mock<IUserRegisterModel>();

            var registerUserCmd = new RegisterUserCommand(userServiceStub.Object, userSessionMock.Object, dtoFactoryMock.Object, validatorMock.Object, writerStub.Object, readerMock.Object, hasherMock.Object);

            hasherMock.Setup(h => h.CreatePassword(password)).Returns(hashMock);

            userSessionMock.Setup(us => us.HasSomeoneLogged()).Returns(false);

            dtoFactoryMock.Setup(dtoFac => dtoFac.CreateUserRegisterModel(It.IsAny<string>(), It.IsAny<string>(), hashMock, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(userModelStub.Object);

            validatorMock.Setup(v => v.IsValid(userModelStub.Object)).Returns(false);

            readerMock.SetupSequence(r => r.Read())
                .Returns(It.IsAny<string>())
                .Returns(It.IsAny<string>())
                .Returns(password)
                .Returns(password);

            // Act
            Action executingRegisterUserCmd = () => registerUserCmd.ExecuteThisCommand();

            // Assert
            Assert.ThrowsException<ArgumentException>(executingRegisterUserCmd);
        }
    }
}
