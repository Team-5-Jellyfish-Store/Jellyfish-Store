using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
    }
}
