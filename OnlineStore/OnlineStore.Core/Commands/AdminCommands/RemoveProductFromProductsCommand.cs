using System;
using System.Linq;
using OnlineStore.Core.Contracts;
using OnlineStore.Data.Contracts;
using OnlineStore.Models.DataModels;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Core.Commands.AdminCommands
{
    public class RemoveProductFromProductsCommand : ICommand
    {
        private readonly IProductService productService;
        private readonly IUserSessionService sessionService;
        private readonly IReader reader;
        private readonly IWriter writer;

        public RemoveProductFromProductsCommand(IProductService productService, IUserSessionService sessionService, IReader reader, IWriter writer)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.sessionService = sessionService ?? throw new ArgumentNullException(nameof(IUserSessionService));
            this.reader = reader ?? throw new ArgumentNullException(nameof(IReader));
            this.writer = writer ?? throw new ArgumentNullException(nameof(IWriter));
        }
        public string ExecuteThisCommand()
        {

            if (this.sessionService.UserIsAdmin() || this.sessionService.UserIsModerator())
            {
                this.writer.Write("Please enter product name: ");
                var productName = this.reader.Read();
                productService.RemoveProductByName(productName);
                return $"Product {productName} removed successfully!";
            }
            else
            {
                return "User is neither admin nor moderator and cannot add products!";
            }
        }
    }
}
