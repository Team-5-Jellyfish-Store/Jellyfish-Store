using Autofac;
using OnlineStore.Core.Contracts;
using OnlineStore.Common.AutoMapperConfig;
using OnlineStore.App.AutofacConfig;

namespace OnlineStore.App
{
    public class StartUp
    {
        static void Main()
        {
            AutomapperConfiguration.Initialize();
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacConfiguration());
            var container = builder.Build();

            var engine = container.Resolve<IEngine>();
            engine.Run();
        }
    }
}
