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

            // Act
            Action executingRegisterCmd = () => registerUserCmd.ExecuteThisCommand();

            // Assert
            Assert.ThrowsException<ArgumentException>(executingRegisterCmd);
        }
    }
}
