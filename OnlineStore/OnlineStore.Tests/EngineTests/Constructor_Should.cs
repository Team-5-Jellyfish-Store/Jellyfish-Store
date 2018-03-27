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
            var fakeFileReader = new Mock<IFileReader>();
            
            //Act && Assert
            Assert.IsInstanceOfType(new Engine(fakeParser.Object, fakeProcessor.Object, fakeWriter.Object, fakeReader.Object, fakeFileReader.Object), typeof(IEngine));
        }


        [TestMethod]
        public void Throw_WhenParserIsNull()
        {
            //Arrange
            var fakeProcessor = new Mock<ICommandProcessor>();
            var fakeWriter = new Mock<IWriter>();
            var fakeReader = new Mock<IReader>();
            var fakeFileReader = new Mock<IFileReader>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Engine(null, fakeProcessor.Object, fakeWriter.Object, fakeReader.Object, fakeFileReader.Object));
        }

        [TestMethod]
        public void Throw_WhenProcessorIsNull()
        {
            //Arrange
            var fakeParser = new Mock<ICommandParser>();
            var fakeWriter = new Mock<IWriter>();
            var fakeReader = new Mock<IReader>();
            var fakeFileReader = new Mock<IFileReader>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Engine(fakeParser.Object, null, fakeWriter.Object, fakeReader.Object, fakeFileReader.Object));
        }

        [TestMethod]
        public void Throw_WhenWriterIsNull()
        {
            //Arrange
            var fakeParser = new Mock<ICommandParser>();
            var fakeProcessor = new Mock<ICommandProcessor>();
            var fakeReader = new Mock<IReader>();
            var fakeFileReader = new Mock<IFileReader>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Engine(fakeParser.Object, fakeProcessor.Object, null, fakeReader.Object, fakeFileReader.Object));
        }

        [TestMethod]
        public void Throw_WhenReaderIsNull()
        {
            //Arrange
            var fakeParser = new Mock<ICommandParser>();
            var fakeProcessor = new Mock<ICommandProcessor>();
            var fakeWriter = new Mock<IWriter>();
            var fakeFileReader = new Mock<IFileReader>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Engine(fakeParser.Object, fakeProcessor.Object, fakeWriter.Object, null, fakeFileReader.Object));
        }
        [TestMethod]
        public void Throw_WhenFileReaderIsNull()
        {
            //Arrange
            var fakeParser = new Mock<ICommandParser>();
            var fakeProcessor = new Mock<ICommandProcessor>();
            var fakeReader = new Mock<IReader>();
            var fakeWriter = new Mock<IWriter>();
            var fakeFileReader = new Mock<IFileReader>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Engine(fakeParser.Object, fakeProcessor.Object, fakeWriter.Object, fakeReader.Object, null));
        }
        [TestMethod]
        public void SetsTheRightParserToProperty()
        {
            // Arrange, Act
            var stubParser = new Mock<ICommandParser>();
            var stubWriter = new Mock<IWriter>();
            var stubReader = new Mock<IReader>();
            var stubProcessor = new Mock<ICommandProcessor>();
            var stubFileReader = new Mock<IFileReader>();

            var engine = new MockEngine(stubParser.Object, stubProcessor.Object, stubWriter.Object, stubReader.Object, stubFileReader.Object);

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
            var stubFileReader = new Mock<IFileReader>();

            var engine = new MockEngine(stubParser.Object, stubProcessor.Object, stubWriter.Object, stubReader.Object, stubFileReader.Object);

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
            var stubFileReader = new Mock<IFileReader>();

            var engine = new MockEngine(stubParser.Object, stubProcessor.Object, stubWriter.Object, stubReader.Object, stubFileReader.Object);

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
            var stubFileReader = new Mock<IFileReader>();

            var engine = new MockEngine(stubParser.Object, stubProcessor.Object, stubWriter.Object, stubReader.Object, stubFileReader.Object);

            // Assert
            Assert.AreSame(stubProcessor.Object, engine.ExposedCommandProcessor);
        }

        [TestMethod]
        public void SetsTheRightFileReaderToProperty()
        {
            // Arrange, Act
            var stubParser = new Mock<ICommandParser>();
            var stubWriter = new Mock<IWriter>();
            var stubReader = new Mock<IReader>();
            var stubProcessor = new Mock<ICommandProcessor>();
            var stubFileReader = new Mock<IFileReader>();

            var engine = new MockEngine(stubParser.Object, stubProcessor.Object, stubWriter.Object, stubReader.Object, stubFileReader.Object);

            // Assert
            Assert.AreSame(stubFileReader.Object, engine.ExposedFileReader);
        }
    }
}
