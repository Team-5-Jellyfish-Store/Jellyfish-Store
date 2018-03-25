using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Contracts;
using OnlineStore.Tests.Mocks;

namespace OnlineStore.Tests.Factories.CommandParser
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnInstance_WhenProvidedValidParameters()
        {
            //Arrange
            var fakeCmdFactory = new Mock<ICommandFactory>();

            //Act && Assert
            Assert.IsInstanceOfType(new Core.Factories.CommandParser(fakeCmdFactory.Object), typeof(ICommandParser));
        }

       [TestMethod]
        public void ThrowArgumentNullException_WhenCmdFactoryIsNull()
        {
            // Arrange, Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Core.Factories.CommandParser(null));
        }

        [TestMethod]
        public void SetsTheRightFactoryToProperty()
        {
            // Arrange, Act
            var stubFactory = new Mock<ICommandFactory>();
            var parser = new MockCommandParser(stubFactory.Object);

            // Assert
            Assert.AreSame(stubFactory.Object, parser.ExposedCommandFactory);
        }
    }
}
