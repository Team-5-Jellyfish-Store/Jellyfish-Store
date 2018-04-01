using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Tests.Commands.Help
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnInstance_WhenProvidedValidParameters()
        {
            //Arrange
            var fakeFileReader = new Mock<IFileReader>();
            
            //Act && Assert
            Assert.IsInstanceOfType(new HelpCommand(fakeFileReader.Object), typeof(HelpCommand));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenFileReaderIsNull()
        {
            //Arrange && Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new HelpCommand(null));
        }
    }
}
