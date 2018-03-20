using OnlineStore.Core.Contracts;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using OnlineStore.Data.Contracts;
using System.Linq;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Core.Commands
{
    public class PrintAvailableProductReportCommand : ICommand
    {
        private readonly IProductService productService;

        public PrintAvailableProductReportCommand(IProductService productService)
        {
            this.productService = productService;
        }
        public string ExecuteThisCommand()
        {
            var products = productService.GetAllProducts();
            string uniqueName= 
                ($"y{DateTime.Now.Year}m{DateTime.Now.Month}d{DateTime.Now.Day}" +
                $"h{DateTime.Now.Hour}m{DateTime.Now.Minute}s{DateTime.Now.Second}.pdf");
            string fileName = $"../../../OnlineStore.Core/PDFReports/ProductsReport{uniqueName}";
            FileStream fs = new FileStream(fileName, FileMode.Create);
            // Create an instance of the document class which represents the PDF document itself.
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            // Create an instance to the PDF file by creating an instance of the PDF 
            // Writer class using the document and the filestrem in the constructor.
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.AddSubject("This is a PDF showing current products");
            // Open the document to enable you to write to the document

            document.Open();
            // Add a simple and wellknown phrase to the document in a flow layout manner
            document.Add(new Paragraph("Those are the current products:"));
            document.Add(new Paragraph("Name / CategoryName / Quantity / SellingPrice"));
            foreach (var item in products)
            {
                if (item.Quantity>0)
                {
                    document.Add(new Paragraph($"{item.Name} {item.CategoryName} {item.Quantity} {item.SellingPrice}"));
                   
                }
            }

            // Close the document
            document.Close();
            // Close the writer instance
            writer.Close();
            // Always close open filehandles explicity
            fs.Close();
            return "Export products successful!";
            
        }
        
    }
}
