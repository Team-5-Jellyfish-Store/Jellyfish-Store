using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Contracts;
using OnlineStore.Tests.Mocks;

namespace OnlineStore.Tests.Factories.CommandFactory
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnInstance_WhenProvidedValidParameters()
        {
            //Arrange
            var fakeComponentContext = new Mock<IComponentContext>();

            //Act && Assert
            Assert.IsInstanceOfType(new Core.Factories.CommandFactory(fakeComponentContext.Object), typeof(ICommandFactory));
        }

        [TestMethod]
        public void Throw_WhenComponentContextIsNull()
        {
           //Arrange && Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Core.Factories.CommandFactory(null));
        }

        [TestMethod]
        public void SetsTheRightContainerToProperty()
        {
            // Arrange, Act
            var stubContainer = new Mock<IComponentContext>();
            var parser = new MockCommandFactory(stubContainer.Object);

            // Assert
            Assert.AreSame(stubContainer.Object, parser.ExposedContainer);
        }
    }
}
