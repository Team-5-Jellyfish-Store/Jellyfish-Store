using Autofac;
using OnlineStore.Core;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Commands.AdminCommands;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Factories;
using OnlineStore.Core.Providers;
using OnlineStore.Core.Security;
using OnlineStore.Core.ShoppingCartRepository;
using OnlineStore.Core.UserService;
using OnlineStore.Data;
using OnlineStore.Data.Contracts;


namespace OnlineStore.App.AutofacConfig
{
    internal class AutofacConfig : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandFactory>().As<ICommandFactory>().SingleInstance();
            builder.RegisterType<CommandParser>().As<ICommandParser>().SingleInstance();
            builder.RegisterType<CommandProcessor>().As<ICommandProcessor>().SingleInstance();
            builder.RegisterType<OnlineStoreFactory>().As<IOnlineStoreFactory>().SingleInstance();
            builder.RegisterType<ConsoleReader>().As<IReader>().SingleInstance();
            builder.RegisterType<ConsoleWriter>().As<IWriter>().SingleInstance();
            builder.RegisterType<Hasher>().As<IHasher>().SingleInstance();
            builder.RegisterType<Validator>().As<IValidator>().SingleInstance();

            builder.RegisterType<ShoppingCartRepository>().As<IShoppingRepository>().SingleInstance();
            builder.RegisterType<OnlineStoreContext>().As<IOnlineStoreContext>().InstancePerDependency();
            builder.RegisterType<UserSessionService>().As<IUserSessionService>().SingleInstance();
            builder.RegisterType<Engine>().As<IEngine>().SingleInstance();

            //Commands
            builder.RegisterType<RegisterClientCommand>().Named<ICommand>("register");
            builder.RegisterType<LoginCommand>().Named<ICommand>("login");
            builder.RegisterType<ExitCommand>().Named<ICommand>("exit");
            builder.RegisterType<AddProductToProductsCommand>().Named<ICommand>("addProduct");
            builder.RegisterType<ImportCouriersCommand>().Named<ICommand>("importCouriers");
            builder.RegisterType<ImportProductsCommand>().Named<ICommand>("importProducts");

            builder.RegisterType<SearchCategoryCommand>().Named<ICommand>("searchByCategory");
            builder.RegisterType<SearchProductCommand>().Named<ICommand>("searchByName");






        }
    }
}
