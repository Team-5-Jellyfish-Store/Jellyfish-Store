using iTextSharp.text;
using iTextSharp.text.pdf;
using OnlineStore.DTO.OrderModels.Constracts;
using OnlineStore.DTO.ProductModels.Contracts;
using OnlineStore.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OnlineStore.Providers.Providers
{
    public class PDFExporter:IPDFExporter
    {
        public void ExportProducts(IEnumerable<IProductModel> products)
        {
            string uniqueName =
                ($"y{DateTime.Now.Year}m{DateTime.Now.Month}d{DateTime.Now.Day}" +
                $"h{DateTime.Now.Hour}m{DateTime.Now.Minute}s{DateTime.Now.Second}.pdf");
            string fileName = $"../../../PDFReports/ProductsReport{uniqueName}";
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
            if (products.Count() == 0)
            {
                document.Add(new Paragraph($"No products at the moment"));

            }
            foreach (var item in products)
            {
                if (item.Quantity > 0)
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
            
        }
        public void ExportOrders(IEnumerable<IOrderModel> orders)
        {
            string uniqueName =
                    ($"y{DateTime.Now.Year}m{DateTime.Now.Month}d{DateTime.Now.Day}" +
                    $"h{DateTime.Now.Hour}m{DateTime.Now.Minute}s{DateTime.Now.Second}.pdf");
            string fileName = $"../../../PDFReports/OrdersReport{uniqueName}";
            FileStream fs = new FileStream(fileName, FileMode.Create);
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.AddSubject("This is a PDF showing current orders");

            document.Open();
            document.Add(new Paragraph("Those are the current orders:"));
            document.Add(new Paragraph(" Comment / ClientName / OrderedOn"));
            if (orders.Count() == 0)
            {
                document.Add(new Paragraph($"No orders at the moment"));

            }
            foreach (var item in orders)
            {
                document.Add(new Paragraph($"{item.Comment} {item.Username} {item.OrderedOn}"));
            }

            document.Close();
            writer.Close();
            fs.Close();
        }
    }
}
