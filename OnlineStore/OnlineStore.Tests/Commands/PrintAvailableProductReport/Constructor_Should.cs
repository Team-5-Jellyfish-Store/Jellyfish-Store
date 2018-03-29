using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;
using OnlineStore.Core.Commands;

namespace OnlineStore.Tests.Commands.PrintAvailableProductReport
{

    [TestClass]
    public class Constructor_Should
    {

        [TestMethod]
        public void ReturnInstance_WhenProvidedValidParameters()
        {
            //Arrange
            var stubProductService = new Mock<IProductService>();
            var stubPDFExporter = new Mock<IPDFExporter>();

            //Act
            var testedCommand = new PrintAvailableProductReportCommand(stubProductService.Object, stubPDFExporter.Object);
            //Assert
            Assert.IsInstanceOfType(testedCommand, typeof(PrintAvailableProductReportCommand));
        }

        [TestMethod]
        public void Throw_WhenProductServiceIsNull()
        {
            //Arrange
            var stubPDFExporter = new Mock<IPDFExporter>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new PrintAvailableProductReportCommand(null,stubPDFExporter.Object));
        }

        [TestMethod]
        public void Throw_WhenPDFExporterIsNull()
        {
            //Arrange
            var stubProductService = new Mock<IProductService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new PrintAvailableProductReportCommand(stubProductService.Object, null));
        }
    }
}
