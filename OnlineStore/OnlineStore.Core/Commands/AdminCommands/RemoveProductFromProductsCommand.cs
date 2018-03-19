using System;
using System.Linq;
using OnlineStore.Core.Contracts;
using OnlineStore.Data.Contracts;
using OnlineStore.Models.DataModels;

namespace OnlineStore.Core.Commands.AdminCommands
{
    public class RemoveProductFromProductsCommand : ICommand
    {

        private readonly IUserSessionService sessionService;
        private readonly IOnlineStoreContext context;
        private readonly IReader reader;
        private readonly IWriter writer;

        public RemoveProductFromProductsCommand(IUserSessionService sessionService, IOnlineStoreContext context, IReader reader, IWriter writer)
        {
            this.sessionService = sessionService ?? throw new ArgumentNullException(nameof(IUserSessionService));
            this.context = context ?? throw new ArgumentNullException(nameof(IOnlineStoreContext));
            this.reader = reader ?? throw new ArgumentNullException(nameof(IReader));
            this.writer = writer ?? throw new ArgumentNullException(nameof(IWriter));
        }
        public string ExecuteThisCommand()
        {

            if (this.sessionService.UserIsAdmin() || this.sessionService.UserIsModerator())
            {
                this.writer.Write("Please enter product name: ");
                var productName = this.reader.Read();
                var productForRemoval = this.context.Products.FirstOrDefault(f => f.Name == productName);
                if (productForRemoval != null)
                {
                    this.context.Products.Remove(productForRemoval);
                    this.context.SaveChanges();
                    return $"Product {productName} removed successfully!";
                }
                return $"Product {productName} not found!";
            }
            else
            {
                return "User is neither admin nor moderator and cannot add products!";
            }
        }
    }
}
