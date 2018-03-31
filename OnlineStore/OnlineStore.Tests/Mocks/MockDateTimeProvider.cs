using System;
using OnlineStore.Core.Providers.Providers;

namespace OnlineStore.Tests.Mocks
{
    public class MockDateTimeProvider : DatetimeProvider
    {
        public override DateTime Now => new DateTime(2018, 03, 15, 12, 15, 11);
    }
}
