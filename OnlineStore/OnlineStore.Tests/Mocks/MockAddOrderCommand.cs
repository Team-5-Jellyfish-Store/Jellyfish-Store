using System.Collections.Generic;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Providers.Providers;
using OnlineStore.DTO.Factory;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;
using System;

namespace OnlineStore.Tests.Mocks
{
    public class MockAddOrderCommand : AddOrderCommand
    {
        public MockAddOrderCommand(IOrderService orderService, IProductService productService, IUserSession userSession, IDataTransferObjectFactory dataTransferObjectFactory, IValidator validator, IWriter writer, IReader reader, DatetimeProvider datetime) : base(orderService, productService, userSession, dataTransferObjectFactory, validator, writer, reader, datetime)
        {
        }

        public IDictionary<string, int> ExposedProductNameAndCounts => new Dictionary<string, int>(base.ProductNameAndCounts);
    }
}
