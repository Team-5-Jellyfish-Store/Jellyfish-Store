using Autofac;
using OnlineStore.Core.Contracts;
using OnlineStore.Common.AutoMapperConfig;
using AutoMapper;
using OnlineStore.DTO.SupplierModels;
using OnlineStore.Models.DataModels;

namespace OnlineStore.App
{
    public class StartUp
    {
        static void Main()
        {
            AutomapperConfiguration.Initialize();

            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacConfig.AutofacConfiguration());
            var container = builder.Build();

            var engine = container.Resolve<IEngine>();
            engine.Run();
        }
    }
}
