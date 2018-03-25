using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core;
using OnlineStore.Core.Contracts;
using OnlineStore.Tests.Mocks;

namespace OnlineStore.Tests.EngineTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnInstance_WhenProvidedCorrectParameters()
        {
            //Arrange
            var fakeParser = new Mock<ICommandParser>();
            var fakeProcessor = new Mock<ICommandProcessor>();
            var fakeWriter = new Mock<IWriter>();
            var fakeReader = new Mock<IReader>();
            
            //Act && Assert
            Assert.IsInstanceOfType(new Engine(fakeParser.Object, fakeProcessor.Object, fakeWriter.Object, fakeReader.Object), typeof(IEngine));
        }


        [TestMethod]
        public void Throw_WhenParserIsNull()
        {
            //Arrange
            var fakeProcessor = new Mock<ICommandProcessor>();
            var fakeWriter = new Mock<IWriter>();
            var fakeReader = new Mock<IReader>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Engine(null, fakeProcessor.Object, fakeWriter.Object, fakeReader.Object));
        }

        [TestMethod]
        public void Throw_WhenProcessorIsNull()
        {
            //Arrange
            var fakeParser = new Mock<ICommandParser>();
            var fakeWriter = new Mock<IWriter>();
            var fakeReader = new Mock<IReader>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Engine(fakeParser.Object, null, fakeWriter.Object, fakeReader.Object));
        }

        [TestMethod]
        public void Throw_WhenWriterIsNull()
        {
            //Arrange
            var fakeParser = new Mock<ICommandParser>();
            var fakeProcessor = new Mock<ICommandProcessor>();
            var fakeReader = new Mock<IReader>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Engine(fakeParser.Object, fakeProcessor.Object, null, fakeReader.Object));
        }

        [TestMethod]
        public void Throw_WhenReaderIsNull()
        {
            //Arrange
            var fakeParser = new Mock<ICommandParser>();
            var fakeProcessor = new Mock<ICommandProcessor>();
            var fakeWriter = new Mock<IWriter>();
            
            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Engine(fakeParser.Object, fakeProcessor.Object, fakeWriter.Object, null));
        }

        [TestMethod]
        public void SetsTheRightParserToProperty()
        {
            // Arrange, Act
            var stubParser = new Mock<ICommandParser>();
            var stubWriter = new Mock<IWriter>();
            var stubReader = new Mock<IReader>();
            var stubProcessor = new Mock<ICommandProcessor>();
            var engine = new MockEngine(stubParser.Object, stubProcessor.Object, stubWriter.Object, stubReader.Object);

            // Assert
            Assert.AreSame(stubParser.Object, engine.ExposedCommandParser);
        }

        [TestMethod]
        public void SetsTheRightWriterToProperty()
        {
            // Arrange, Act
            var stubParser = new Mock<ICommandParser>();
            var stubWriter = new Mock<IWriter>();
            var stubReader = new Mock<IReader>();
            var stubProcessor = new Mock<ICommandProcessor>();
            var engine = new MockEngine(stubParser.Object, stubProcessor.Object, stubWriter.Object, stubReader.Object);

            // Assert
            Assert.AreSame(stubWriter.Object, engine.ExposedWriter);
        }

        [TestMethod]
        public void SetsTheRightReaderToProperty()
        {
            // Arrange, Act
            var stubParser = new Mock<ICommandParser>();
            var stubWriter = new Mock<IWriter>();
            var stubReader = new Mock<IReader>();
            var stubProcessor = new Mock<ICommandProcessor>();
            var engine = new MockEngine(stubParser.Object, stubProcessor.Object, stubWriter.Object, stubReader.Object);

            // Assert
            Assert.AreSame(stubReader.Object, engine.ExposedReader);
        }

        [TestMethod]
        public void SetsTheRightProcessorToProperty()
        {
            // Arrange, Act
            var stubParser = new Mock<ICommandParser>();
            var stubWriter = new Mock<IWriter>();
            var stubReader = new Mock<IReader>();
            var stubProcessor = new Mock<ICommandProcessor>();
            var engine = new MockEngine(stubParser.Object, stubProcessor.Object, stubWriter.Object, stubReader.Object);

            // Assert
            Assert.AreSame(stubProcessor.Object, engine.ExposedCommandProcessor);
        }
    }
}
