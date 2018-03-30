using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Tests.Commands.SearchProduct
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnInstance_WhenProvidedValidParameters()
        {
            //Arrange
            var stubProductService = new Mock<IProductService>();
            var stubReader = new Mock<IReader>();
            var stubWriter = new Mock<IWriter>();

            //Act
            var testedCommand = new SearchProductCommand
                (stubReader.Object, stubWriter.Object, stubProductService.Object);
            //Assert
            Assert.IsInstanceOfType(testedCommand, typeof(SearchProductCommand));
        }

        [TestMethod]
        public void Throw_WhenProductServiceIsNull()
        {
            //Arrange
            var stubReader = new Mock<IReader>();
            var strubWriter = new Mock<IWriter>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>
                (() => new SearchProductCommand(stubReader.Object, strubWriter.Object, null));
        }

        [TestMethod]
        public void Throw_WhenReaderIsNull()
        {
            //Arrange
            var stubProductService = new Mock<IProductService>();
            var strubWriter = new Mock<IWriter>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>
                (() => new SearchProductCommand(null, strubWriter.Object, stubProductService.Object));
        }

        [TestMethod]
        public void Throw_WhenWriterIsNull()
        {
            //Arrange
            var stubProductService = new Mock<IProductService>();
            var stubReader = new Mock<IReader>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>
                (() => new SearchProductCommand(stubReader.Object, null, stubProductService.Object));
        }
    }


}
