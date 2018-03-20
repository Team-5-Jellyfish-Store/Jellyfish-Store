using OnlineStore.Core.Contracts;
using OnlineStore.Data.Contracts;
using OnlineStore.Models.DataModels;
using System;
using System.Linq;

namespace OnlineStore.Core.Commands
{
    public class AddOrderCommand : ICommand
    {
        private readonly IOnlineStoreContext context;
        private readonly IValidator validator;
        private readonly IUserSessionService userSession;
        private readonly IWriter writer;
        private readonly IReader reader;

        public AddOrderCommand(IOnlineStoreContext context, IValidator validator, IUserSessionService userSession, IWriter writer, IReader reader)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public string ExecuteThisCommand()
        {
            var loggedUser = userSession.GetLoggedUser() ?? throw new ArgumentException("No logged user!");
            var user = context.Users.SingleOrDefault(x => x.Username == loggedUser);

            this.writer.Write("Product: ");
            string productName = this.reader.Read();
            productName = validator.ValidateValue(productName, true);

            this.writer.Write("Count: ");
            int productCount = int.Parse(this.reader.Read());
            validator.ValidateLength(productCount, 1, 1000);

            if (!this.context.Products.Any(x => x.Name == productName))
            {
                throw new ArgumentException("Product with that name don't exists!");
            }

            if (this.context.Products.Any(x => x.Quantity > productCount))
            {
                throw new ArgumentException("Product quantity...");
            }

            var order = new Order()
            {
                OrderedOn = DateTime.Now,
                User = user,
                Courier = context.Couriers.FirstOrDefault(),
                ProductsCount = productCount
            };

            user.Orders.Add(order);

            context.SaveChanges();

            return "Order Added";
        }
    }
}
