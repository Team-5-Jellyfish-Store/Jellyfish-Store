using iTextSharp.text;
using iTextSharp.text.pdf;
using OnlineStore.Core.Contracts;
using OnlineStore.Data.Contracts;
using System;
using System.IO;
using System.Linq;

namespace OnlineStore.Core.Commands
{
    public class PrintOrdersReportCommand : ICommand
    {
        private IOnlineStoreContext context;
        private readonly IUserSessionService sessionService;

        public PrintOrdersReportCommand(IOnlineStoreContext context, IUserSessionService sessionService)
        {
            this.context = context;
            this.sessionService = sessionService;
        }

        

        public string ExecuteThisCommand()
        {
            if (this.sessionService.UserIsAdmin() || this.sessionService.UserIsModerator())
            {
                var orders = context.Orders.ToList();
                string uniqueName =
                    ($"y{DateTime.Now.Year}m{DateTime.Now.Month}d{DateTime.Now.Day}" +
                    $"h{DateTime.Now.Hour}m{DateTime.Now.Minute}s{DateTime.Now.Second}.pdf");
                string fileName = $"../../../OnlineStore.Core/PDFReports/OrdersReport{uniqueName}";
                FileStream fs = new FileStream(fileName, FileMode.Create);
                // Create an instance of the document class which represents the PDF document itself.
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                // Create an instance to the PDF file by creating an instance of the PDF 
                // Writer class using the document and the filestrem in the constructor.
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.AddSubject("This is a PDF showing current orders");
                // Open the document to enable you to write to the document

                document.Open();
                // Add a simple and wellknown phrase to the document in a flow layout manner
                document.Add(new Paragraph("Those are the current orders:"));
                document.Add(new Paragraph("Id / Comment / ClientName / ItemDeliveredOn"));
                foreach (var item in orders)
                {
                    document.Add(new Paragraph($"{item.Id} {item.Comment} {item.User.FirstName}{item.User.LastName} {item.DeliveredOn}"));
                }

                // Close the document
                document.Close();
                // Close the writer instance
                writer.Close();
                // Always close open filehandles explicity
                fs.Close();
                return "Exported Successful";
            }
            return "User must be admin or moderator in order to export data!";
        }
    }
}
