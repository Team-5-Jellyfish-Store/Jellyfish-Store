using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;
using OnlineStore.DTO.OrderModels.Constracts;
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
    public class ExecuteThisCommand_Should
    {
        [TestMethod]
        public void Invoke_UserSession_CheckRights()
        {
            //Arrange
            var stubOrderService = new Mock<IOrderService>();
            var mockUserSession = new Mock<IUserSession>();
            var stubPDFExporter = new Mock<IPDFExporter>();
            var testedCommand = new PrintOrdersReportCommand(stubOrderService.Object, mockUserSession.Object, stubPDFExporter.Object);

            //Act
            testedCommand.ExecuteThisCommand();
            //Assert
            mockUserSession.Verify(x => x.HasAdminRights());
        }

        [TestMethod]
        public void ReturnCorrect_WhenUserIsNotAdmin()
        {
            //Arrange
            var stubOrderService = new Mock<IOrderService>();
            var stubUserSession = new Mock<IUserSession>();
            var stubPDFExporter = new Mock<IPDFExporter>();
            var testedCommand = new PrintOrdersReportCommand(stubOrderService.Object, stubUserSession.Object, stubPDFExporter.Object);
            var expected = "User must be admin or moderator in order to export data!";

            //Act
            stubUserSession.Setup(x => x.HasAdminRights()).Returns(false);
            var actual = testedCommand.ExecuteThisCommand();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Invoke_OrderServiceGetAll()
        {
            //Arrange
            var mockOrderService = new Mock<IOrderService>();
            var stubUserSession = new Mock<IUserSession>();
            stubUserSession.Setup(x => x.HasAdminRights()).Returns(true);
            var stubPDFExporter = new Mock<IPDFExporter>();
            var testedCommand = new PrintOrdersReportCommand(mockOrderService.Object, stubUserSession.Object, stubPDFExporter.Object);

            //Act
            var actual = testedCommand.ExecuteThisCommand();

            //Assert
            mockOrderService.Verify(x => x.GetAllOrders(), Times.Once);
        }

        [TestMethod]
        public void Invoke_PDFExporterExportOrders()
        {
            //Arrange
            var stubOrderService = new Mock<IOrderService>();
            var stubUserSession = new Mock<IUserSession>();
            stubUserSession.Setup(x => x.HasAdminRights()).Returns(true);
            var mockPDFExporter = new Mock<IPDFExporter>();
            var testedCommand = new PrintOrdersReportCommand(stubOrderService.Object, stubUserSession.Object, mockPDFExporter.Object);
            var fakeCollection = new List<IOrderModel>().AsEnumerable();
            //Act
            testedCommand.ExecuteThisCommand();
            //Assert
            mockPDFExporter.Verify(x => x.ExportOrders(fakeCollection));
        }

        [TestMethod]
        public void ReturnCorrect_WhenUserIsAdmin()
        {
            //Arrange
            var stubOrderService = new Mock<IOrderService>();
            var stubUserSession = new Mock<IUserSession>();
            var stubPDFExporter = new Mock<IPDFExporter>();
            var testedCommand = new PrintOrdersReportCommand(stubOrderService.Object, stubUserSession.Object, stubPDFExporter.Object);
            var expected = "Exported orders successful";

            //Act
            stubUserSession.Setup(x => x.HasAdminRights()).Returns(true);
            var actual = testedCommand.ExecuteThisCommand();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
