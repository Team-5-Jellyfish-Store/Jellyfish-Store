using OnlineStore.Core.Contracts;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using OnlineStore.Data.Contracts;
using System.Linq;

namespace OnlineStore.Core.Commands
{
    public class PrintAvailableProductReportCommand : ICommand
    {
        private IOnlineStoreContext context;
        private IReader reader;
        private IWriter writer;

        public PrintAvailableProductReportCommand(IOnlineStoreContext context, IReader reader, IWriter writer)
        {
            this.context = context;
            this.reader = reader;
            this.writer = writer;
        }
        public string ExecuteThisCommand()
        {
            var orders = context.Orders.ToList();
            string uniqueName= 
                ($"y{DateTime.Now.Year}m{DateTime.Now.Month}d{DateTime.Now.Day}" +
                $"h{DateTime.Now.Hour}m{DateTime.Now.Minute}s{DateTime.Now.Second}.pdf");
            string fileName = $"../../../OnlineStore.Core/PDFReports/ReportAboutTheOrders{uniqueName}";
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
            foreach (var item in orders)
            {
                document.Add(new Paragraph($"w{item.Id}a{item.Comment}b{item.User.FirstName}c{item.User.LastName}d{item.DeliveredOn}"));
            }

            // Close the document
            document.Close();
            // Close the writer instance
            writer.Close();
            // Always close open filehandles explicity
            fs.Close();
            return "";
            
        }
        
    }
}
