using OnlineStore.Core.Contracts;
using System;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;

namespace OnlineStore.Core.Commands
{
    public class PrintAvailableProductReportCommand : ICommand
    {
        private readonly IProductService productService;
        private readonly IPDFExporter pdfExporter;

        public PrintAvailableProductReportCommand(IProductService productService, IPDFExporter pdfExporter)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.pdfExporter = pdfExporter ?? throw new ArgumentNullException(nameof(pdfExporter));
        }

        public string ExecuteThisCommand()
        {
            var products = productService.GetAllProducts();
            pdfExporter.ExportProducts(products);
            return "Export products successful!";
        }
    }
}
