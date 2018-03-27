using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Tests.Factories.CommandParser
{
    [TestClass]
    public class ParseCommand_Should
    {
        [TestMethod]
        public void Throw_WhenProvidedNullInput()
        {
            //Arrange
            var emptyInput = string.Empty;
            var fakeFactory = new Mock<ICommandFactory>();
            var parser = new Core.Factories.CommandParser(fakeFactory.Object);

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => parser.ParseCommand(emptyInput));
        }

        [TestMethod]
        public void InvokeCreateCommand_WhenProvidedCorrectParameter()
        {
            //Arrange
            var fakeFactory = new Mock<ICommandFactory>();
            var parser = new Core.Factories.CommandParser(fakeFactory.Object);

            //Act
            parser.ParseCommand("test");

            //Assert
            fakeFactory.Verify(v => v.CreateCommand(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void ReturnICommand_WhenProvidedValidParameter()
        {
            //Arrange
            var fakeFactory = new Mock<ICommandFactory>();
            var fakeUserSession = new Mock<IUserSession>();
            fakeFactory.Setup(s => s.CreateCommand(It.IsAny<string>())).Returns(new LogoutCommand(fakeUserSession.Object));
            var factory = new Core.Factories.CommandParser(fakeFactory.Object);

            //Act
            var mockCommand = factory.ParseCommand("logout");

            //Assert
            Assert.IsInstanceOfType(mockCommand, typeof(ICommand));
        }
    }
}
