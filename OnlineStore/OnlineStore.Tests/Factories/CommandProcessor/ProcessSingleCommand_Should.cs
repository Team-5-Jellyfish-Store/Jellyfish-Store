using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Tests.Factories.CommandProcessor
{
    [TestClass]
    public class ProcessSingleCommand_Should
    {
        [TestMethod]
        public void InvokeExecuteCommand_WhenProvidedValidParameter()
        {
            //Arrange
            var fakeCommand = new Mock<ICommand>();
            var processor = new Core.Factories.CommandProcessor();

            //Act
            processor.ProcessSingleCommand(fakeCommand.Object);
            
            //Assert

            fakeCommand.Verify(v => v.ExecuteThisCommand(), Times.Once);
        }

        [TestMethod]
        public void Throw_WhenNullArgumentsProvided()
        {
            //Arrange
            var processor = new Core.Factories.CommandProcessor();
            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => processor.ProcessSingleCommand(null));
        }
    }
}
