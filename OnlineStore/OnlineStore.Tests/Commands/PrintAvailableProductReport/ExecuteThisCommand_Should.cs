using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.ProductModels.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Tests.Commands.PrintAvailableProductReport
{
    [TestClass]
    public class ExecuteThisCommand_Should
    {
        [TestMethod]
        public void Invoke_ProductServiceGetAll()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            var stubPDFExporter = new Mock<IPDFExporter>();

            //Act
            var testedCommand = new PrintAvailableProductReportCommand(mockProductService.Object, stubPDFExporter.Object);
            testedCommand.ExecuteThisCommand();
            //Assert
            mockProductService.Verify(x => x.GetAllProducts(), Times.Once);
        }

        [TestMethod]
        public void Invoke_PDFExporterExportProducts()
        {
            //Arrange
            var stubProductService = new Mock<IProductService>();
            var mockPDFExporter = new Mock<IPDFExporter>();
            var fakeCollection = new List<IProductModel>().AsEnumerable();
            var testedCommand = new PrintAvailableProductReportCommand(stubProductService.Object, mockPDFExporter.Object);

            //Act
            testedCommand.ExecuteThisCommand();

            //Assert
            mockPDFExporter.Verify(x => x.ExportProducts(fakeCollection), Times.Once);
        }

        [TestMethod]
        public void ShouldReturn_CorrectMessage()
        {
            //Arrange
            var stubProductService = new Mock<IProductService>();
            var stubPDFExporter = new Mock<IPDFExporter>();
            var testedCommand = new PrintAvailableProductReportCommand(stubProductService.Object, stubPDFExporter.Object);
            var expectedResult = "Export products successful!";
            //Act
            var returnedResult = testedCommand.ExecuteThisCommand();

            //Assert
            Assert.AreEqual(expectedResult, returnedResult);
        }

    }
}
