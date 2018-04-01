using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Tests.Commands.Help
{
    [TestClass]
    public class ExecuteThisCommand_Should
    {
        [TestMethod]
        public void InvokeReadAllText_WhenFileReaderIsValid()
        {
            //Arrange
            var fakeFileReader = new Mock<IFileReader>();
            var command = new HelpCommand(fakeFileReader.Object);
            //Act
            var result = command.ExecuteThisCommand();

            //Assert
            fakeFileReader.Verify(v => v.ReadAllText(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ReturnTheCorrectResult_WhenCommandExecutionFinish()
        {
            //Arrange
            var fakeFileReader = new Mock<IFileReader>();
            var command = new HelpCommand(fakeFileReader.Object);
            fakeFileReader.Setup(s => s.ReadAllText(It.IsAny<string>())).Returns("test");
            var expectedResult = "test";
            //Act
            var actualResult = command.ExecuteThisCommand();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
