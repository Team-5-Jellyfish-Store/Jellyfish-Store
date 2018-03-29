using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Tests.Commands.PrintOrdersReport
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnInstance_WhenProvidedValidParameters()
        {
            //Arrange
            var stubOrderService = new Mock<IOrderService>();
            var stubUserSession = new Mock<IUserSession>();
            var stubPDFExporter = new Mock<IPDFExporter>();

            //Act
            var testedCommand = new PrintOrdersReportCommand(stubOrderService.Object, stubUserSession.Object, stubPDFExporter.Object);
            //Assert
            Assert.IsInstanceOfType(testedCommand, typeof(PrintOrdersReportCommand));
        }

        [TestMethod]
        public void Throw_WhenOrderServiceIsNull()
        {
            //Arrange
            var stubUserSession = new Mock<IUserSession>();
            var stubPDFExporter = new Mock<IPDFExporter>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new PrintOrdersReportCommand(null, stubUserSession.Object, stubPDFExporter.Object));
        }

        [TestMethod]
        public void Throw_WhenUserSessionIsNull()
        {
            //Arrange
            var stubOrderService = new Mock<IOrderService>();
            var stubPDFExporter = new Mock<IPDFExporter>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new PrintOrdersReportCommand(stubOrderService.Object, null, stubPDFExporter.Object));
        }

        [TestMethod]
        public void Throw_WhenPDFExporterIsNull()
        {
            //Arrange
            var stubOrderService = new Mock<IOrderService>();
            var stubUserSession = new Mock<IUserSession>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new PrintOrdersReportCommand(stubOrderService.Object, stubUserSession.Object, null));
        }
    }
}
