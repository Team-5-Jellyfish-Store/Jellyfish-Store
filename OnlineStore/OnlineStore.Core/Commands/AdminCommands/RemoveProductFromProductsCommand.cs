using System;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Core.Commands.AdminCommands
{
    public class RemoveProductFromProductsCommand : ICommand
    {
        private readonly string ProductRemovedSuccesMessage = "Product {0} removed successfully!";
        private readonly string NoLoggedUserFailMessage = "Login first!";
        private readonly string UserHasNoRightsFailMessage = "User is neither admin nor moderator and cannot add products!";

        private readonly IProductService productService;
        private readonly IUserSession userSession;
        private readonly IReader reader;
        private readonly IWriter writer;

        public RemoveProductFromProductsCommand(IProductService productService, IUserSession userSession, IReader reader, IWriter writer)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.userSession = userSession ?? throw new ArgumentNullException(nameof(IUserSession));
            this.reader = reader ?? throw new ArgumentNullException(nameof(IReader));
            this.writer = writer ?? throw new ArgumentNullException(nameof(IWriter));
        }
        public string ExecuteThisCommand()
        {
            if (!this.userSession.HasAdminRights())
            {
                return this.UserHasNoRightsFailMessage;
            }

            this.writer.Write("Please enter product name: ");
            var productName = this.reader.Read();

            this.productService.RemoveProductByName(productName);

            return string.Format(this.ProductRemovedSuccesMessage, productName);
        }
    }
}
