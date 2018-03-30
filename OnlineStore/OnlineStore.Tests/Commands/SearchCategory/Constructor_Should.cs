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

namespace OnlineStore.Tests.Commands.SearchCategory
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
            var testedCommand = new SearchCategoryCommand
                (stubProductService.Object, stubReader.Object, stubWriter.Object);
            //Assert
            Assert.IsInstanceOfType(testedCommand, typeof(SearchCategoryCommand));
        }

        [TestMethod]
        public void Throw_WhenProductServiceIsNull()
        {
            //Arrange
            var stubReader = new Mock<IReader>();
            var strubWriter = new Mock<IWriter>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>
                (() => new SearchCategoryCommand(null, stubReader.Object, strubWriter.Object));
        }

        [TestMethod]
        public void Throw_WhenReaderIsNull()
        {
            //Arrange
            var stubProductService = new Mock<IProductService>();
            var strubWriter = new Mock<IWriter>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>
                (() => new SearchCategoryCommand(stubProductService.Object, null, strubWriter.Object));
        }

        [TestMethod]
        public void Throw_WhenWriterIsNull()
        {
            //Arrange
            var stubProductService = new Mock<IProductService>();
            var stubReader = new Mock<IReader>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>
                (() => new SearchCategoryCommand(stubProductService.Object, stubReader.Object, null));
        }
    }


}
