using Autofac;
using OnlineStore.Core.Contracts;

namespace OnlineStore.App
{
    public class StartUp
    {
        static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacConfig.AutofacConfig());
            var container = builder.Build();

            var engine = container.Resolve<IEngine>();
            engine.Run();
        }
    }
}
