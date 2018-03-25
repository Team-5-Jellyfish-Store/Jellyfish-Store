using Autofac;
using OnlineStore.Core.Factories;

namespace OnlineStore.Tests.Mocks
{
    public class MockCommandFactory : CommandFactory

    {
        public IComponentContext ExposedContainer => this.Container;


        public MockCommandFactory(IComponentContext container) : base(container)
        {
        }
    }
}
