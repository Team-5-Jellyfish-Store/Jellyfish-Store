using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;
using OnlineStore.App.AutofacConfig;

namespace OnlineStore.Tests.Factories.CommandFactory
{
    [TestClass]
    public class CreateCommand_Should
    {
        [TestMethod]
        public void Throw_WhenProvidedNullInput()
        {
            //Arrange
            var emptyInput = string.Empty;
            var fakeContext = new Mock<IComponentContext>();
            var factory = new Core.Factories.CommandFactory(fakeContext.Object);

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => factory.CreateCommand(emptyInput));
        }

        [TestMethod]
        public void ReturnICommand_WhenProvidedValidParameter()
        {
            //Arrange
            var fakeContext = new Mock<IComponentContext>();
            var fakeUserSession = new Mock<IUserSession>();
            fakeContext.Setup(s => s.ResolveNamed<ICommand>(It.IsAny<string>())).Returns(new LogoutCommand(fakeUserSession.Object));
            var factory = new Core.Factories.CommandFactory(fakeContext.Object);

            //Act
            var mockCommand = factory.CreateCommand("logout");

            //Assert
            Assert.IsInstanceOfType(mockCommand, typeof(ICommand));
        }

        [TestMethod]
        public void InvokeResolveNamed_WhenProvidedCorrectParameter()
        { 
            //Arrange
            var mockContext = new Mock<IComponentContext>();
            var factory = new Core.Factories.CommandFactory(mockContext.Object);

            //Act
            factory.CreateCommand("test");

            //Assert
            mockContext.Verify(v => v.ResolveNamed<ICommand>(It.IsAny<string>()), Times.Once());
        }
    }
}
