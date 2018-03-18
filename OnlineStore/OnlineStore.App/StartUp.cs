using Autofac;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.AutomapperConfig;

namespace OnlineStore.App
{
    public class StartUp
    {
        static void Main()
        {
            AutomapperConfiguration.Initialize();
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacConfig.AutofacConfig());
            var container = builder.Build();

            var engine = container.Resolve<IEngine>();
            engine.Run();

            //This is my test, forget about it!
            //var ctx = new OnlineStoreContext();
            //var towns = ctx.Towns.ToList();
            //var testingTown = towns.Find(x => x.Id == 1);
            //testingTown.Name = "Changed";
            //ctx.SaveChanges();
        }
    }
}
