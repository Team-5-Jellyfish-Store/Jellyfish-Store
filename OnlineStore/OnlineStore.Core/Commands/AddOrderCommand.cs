using OnlineStore.Core.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.Core.Commands
{
    public class AddOrderCommand : ICommand
    {
        private readonly IOrderService orderService;
        private readonly IValidator validator;
        private readonly IUserSessionService userSession;
        private readonly IWriter writer;
        private readonly IReader reader;

        public AddOrderCommand(IOrderService orderService, IValidator validator, IUserSessionService userSession, IWriter writer, IReader reader)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public string ExecuteThisCommand()
        {
            var loggedUser = userSession.GetLoggedUser()
                ?? throw new ArgumentException("No logged user!");

            var productNames = new Dictionary<string, int>();

            string yesNo = string.Empty;
            string productName = string.Empty;
            int productCount = new int();
            do
            {
                this.writer.Write("Product: ");
                productName = this.reader.Read();
                validator.ValidateValue(productName, true);

                if (!productNames.ContainsKey(productName))
                {
                    productNames.Add(productName, 0);
                }

                this.writer.Write("Count: ");
                productCount = int.Parse(this.reader.Read());
                validator.ValidateLength(productCount, 1, 1000);

                productNames[productName] += productCount;

                this.writer.Write("More products? (y/n): ");
                yesNo = this.reader.Read();
            } while (yesNo.ToLower() == "y");

            this.writer.Write("Comment: ");
            string comment = this.reader.Read();
            validator.ValidateValue(comment, false);

            var order = new OrderMakeModel()
            {
                Username = loggedUser,
                ProductNameAndCounts = productNames,
                Comment = comment,
                OrderedOn = DateTime.Now
            };

            this.orderService.MakeOrder(order);

            return $"User {loggedUser} ordered on {order.OrderedOn}";
        }
    }
}
