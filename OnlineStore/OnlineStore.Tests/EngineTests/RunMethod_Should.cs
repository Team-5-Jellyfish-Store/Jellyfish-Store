using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Tests.EngineTests
{
    [TestClass]
    public class RunMethod_Should
    {
        [TestMethod]
        public void InvokeParser_WhenProvidedValidParameter()
        {
            //Arrange
            var mockParser = new Mock<ICommandParser>();
            var fakeProcessor = new Mock<ICommandProcessor>();
            var fakeWriter = new Mock<IWriter>();
            var fakeReader = new Mock<IReader>();
            fakeReader.SetupSequence(s => s.Read()).Returns("login").Returns("exit");

            //Act
            IEngine engine = new Engine(mockParser.Object, fakeProcessor.Object, fakeWriter.Object, fakeReader.Object);
            engine.Run();

            //Assert
            mockParser.Verify(v => v.ParseCommand(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void InvokeProcessor_WhenProvidedValidParameter()
        {
            //Arrange
            var fakeParser = new Mock<ICommandParser>();
            var mockProcessor = new Mock<ICommandProcessor>();
            var fakeWriter = new Mock<IWriter>();
            var fakeReader = new Mock<IReader>();
            fakeReader.SetupSequence(s => s.Read()).Returns("login").Returns("exit");
            var fakeCommand = new Mock<ICommand>();
            fakeParser.Setup(s => s.ParseCommand(It.IsAny<string>())).Returns(fakeCommand.Object);

            //Act
            IEngine engine = new Engine(fakeParser.Object, mockProcessor.Object, fakeWriter.Object, fakeReader.Object);
            engine.Run();

            //Assert
            mockProcessor.Verify(v => v.ProcessSingleCommand(It.IsAny<ICommand>()), Times.Once);
        }
    }
}
