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
using OnlineStore.DTO.ProductModels;

namespace OnlineStore.Tests.Commands.SearchCategory
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
            var mockWriter = new Mock<IWriter>();
            var testedCommand = new SearchCategoryCommand
                (stubProductService.Object, stubReader.Object, mockWriter.Object);


            //Act
            testedCommand.ExecuteThisCommand();

            //Assert
            mockWriter.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        public void Invoke_ReaderRead()
        {

            //Arrange
            var stubProductService = new Mock<IProductService>();
            var mockReader = new Mock<IReader>();
            var stubWriter = new Mock<IWriter>();
            var testedCommand = new SearchCategoryCommand
                (stubProductService.Object, mockReader.Object, stubWriter.Object);


            //Act
            testedCommand.ExecuteThisCommand();

            //Assert
            mockReader.Verify(x => x.Read(), Times.Once);

        }

        [TestMethod]
        public void Invoke_ProductServiceGetProductByCategoryName()
        {

            //Arrange
            var mockProductService = new Mock<IProductService>();
            var stubReader = new Mock<IReader>();
            var stubWriter = new Mock<IWriter>();
            var testedCommand = new SearchCategoryCommand
                (mockProductService.Object, stubReader.Object, stubWriter.Object);


            //Act
            testedCommand.ExecuteThisCommand();

            //Assert
            mockProductService.Verify(x => x.GetProductsByCategoryName(It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        public void GetProductsName_IfMatchExist()
        {

            //Arrange
            var stubProductService = new Mock<IProductService>();
            var mockProduct = new Mock<IProductModel>();
            var fakeCollection = new List<IProductModel> { mockProduct.Object }.AsEnumerable();

            stubProductService.Setup(x => x.GetProductsByCategoryName(It.IsAny<string>()))
                .Returns(fakeCollection);
            var stubReader = new Mock<IReader>();
            var stubWriter = new Mock<IWriter>();
            var testedCommand = new SearchCategoryCommand
                (stubProductService.Object, stubReader.Object, stubWriter.Object);


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
            var mockProduct = new Mock<IProductModel>();
            var fakeCollection = new List<IProductModel> { mockProduct.Object }.AsEnumerable();

            stubProductService.Setup(x => x.GetProductsByCategoryName(It.IsAny<string>()))
                .Returns(fakeCollection);
            var stubReader = new Mock<IReader>();
            var stubWriter = new Mock<IWriter>();
            var testedCommand = new SearchCategoryCommand
                (stubProductService.Object, stubReader.Object, stubWriter.Object);


            //Act
            testedCommand.ExecuteThisCommand();

            //Assert
            mockProduct.Verify(x => x.SellingPrice, Times.Once);
        }
    }
}
