using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;
using OnlineStore.DTO.ProductModels.Contracts;
using OnlineStore.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Tests.Commands.SearchProduct
{
    [TestClass]
    public class ExecuteThisCommand_Should
    {
        [TestMethod]
        public void Invoke_WriterWriteLine()
        {

            //Arrange
            var stubProductService = new Mock<IProductService>();
            var stubReader = new Mock<IReader>();
            var stubWriter = new Mock<IWriter>();
            var testedCommand = new SearchProductCommand
                (stubReader.Object, stubWriter.Object, stubProductService.Object);


            //Act
            testedCommand.ExecuteThisCommand();

            //Assert
            stubWriter.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        public void Invoke_ReaderRead()
        {

            //Arrange
            var stubProductService = new Mock<IProductService>();
            var mockReader = new Mock<IReader>();
            var stubWriter = new Mock<IWriter>();
            var testedCommand =  new SearchProductCommand
                (mockReader.Object, stubWriter.Object, stubProductService.Object);


            //Act
            testedCommand.ExecuteThisCommand();

            //Assert
            mockReader.Verify(x => x.Read(), Times.Once);

        }

        [TestMethod]
        public void Invoke_ProductServiceFindProductByName()
        {

            //Arrange
            var mockProductService = new Mock<IProductService>();
            var stubReader = new Mock<IReader>();
            var stubWriter = new Mock<IWriter>();
            var testedCommand = new SearchProductCommand
                (stubReader.Object, stubWriter.Object, mockProductService.Object); 


            //Act
            testedCommand.ExecuteThisCommand();

            //Assert
            mockProductService.Verify(x => x.FindProductByName(It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        public void GetProductsName_IfMatchExist()
        {

            //Arrange
            var stubProductService = new Mock<IProductService>();
            var stubReader = new Mock<IReader>();
            var stubWriter = new Mock<IWriter>();
            var mockProduct = new Mock<IProductModel>();
            stubProductService.Setup(x => x.FindProductByName(It.IsAny<string>()))
                .Returns(mockProduct.Object);
            var testedCommand = new  SearchProductCommand
                (stubReader.Object, stubWriter.Object, stubProductService.Object);

            //Act
            testedCommand.ExecuteThisCommand();

            //Assert
            mockProduct.Verify(x => x.Name, Times.Once);
        }

        [TestMethod]
        public void GetProductsSellingPrice_IfMatchExist()
        {

            //Arrange
            var stubProductService = new Mock<IProductService>();
            var stubReader = new Mock<IReader>();
            var stubWriter = new Mock<IWriter>();
            var mockProduct = new Mock<IProductModel>();
            stubProductService.Setup(x => x.FindProductByName(It.IsAny<string>()))
                .Returns(mockProduct.Object);
            var testedCommand = new SearchProductCommand
                (stubReader.Object, stubWriter.Object, stubProductService.Object);

            //Act
            testedCommand.ExecuteThisCommand();

            //Assert
            mockProduct.Verify(x => x.SellingPrice, Times.Once);
        }

        [TestMethod]
        public void GetProductsCategoryName_IfMatchExist()
        {

            //Arrange
            var stubProductService = new Mock<IProductService>();
            var stubReader = new Mock<IReader>();
            var stubWriter = new Mock<IWriter>();
            var mockProduct = new Mock<IProductModel>();
            stubProductService.Setup(x => x.FindProductByName(It.IsAny<string>()))
                .Returns(mockProduct.Object);
            var testedCommand = new SearchProductCommand
                (stubReader.Object, stubWriter.Object, stubProductService.Object);

            //Act
            testedCommand.ExecuteThisCommand();

            //Assert
            mockProduct.Verify(x => x.CategoryName, Times.Once);
        }
    }
}
