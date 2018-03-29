using iTextSharp.text;
using iTextSharp.text.pdf;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;
using System;
using System.IO;

namespace OnlineStore.Core.Commands
{
    public class PrintOrdersReportCommand : ICommand
    {
        private readonly IOrderService orderService;
        private readonly IUserSession userSession;
        private readonly IPDFExporter pdfExporter;

        public PrintOrdersReportCommand(IOrderService orderService, IUserSession userSession, IPDFExporter pdfExporter)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
            this.pdfExporter = pdfExporter ?? throw new ArgumentNullException(nameof(pdfExporter));
        }

        public string ExecuteThisCommand()
        {
           
            if (this.userSession.HasAdminRights())
            {
                var orders = orderService.GetAllOrders();
                pdfExporter.ExportOrders(orders);
                return "Exported orders successful";
            }
            return "User must be admin or moderator in order to export data!";
        }
    }
}
