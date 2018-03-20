using Autofac;
using OnlineStore.Core;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Commands.AdminCommands;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Factories;
using OnlineStore.Core.Providers;
using OnlineStore.Core.UserSession;
using OnlineStore.Data;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.App.AutofacConfig
{
    internal class AutofacConfig : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandFactory>().As<ICommandFactory>();
            builder.RegisterType<CommandParser>().As<ICommandParser>();
            builder.RegisterType<CommandProcessor>().As<ICommandProcessor>();
            //builder.RegisterType<OnlineStoreFactory>().As<IOnlineStoreFactory>();
            builder.RegisterType<ConsoleReader>().As<IReader>();
            builder.RegisterType<ConsoleWriter>().As<IWriter>();
            builder.RegisterType<Hasher>().As<IHasher>();
            builder.RegisterType<Validator>().As<IValidator>();

            //builder.RegisterType<ShoppingCartRepository>().As<IShoppingRepository>().SingleInstance();
            builder.RegisterType<OnlineStoreContext>().As<IOnlineStoreContext>().InstancePerDependency();
            builder.RegisterType<UserSessionService>().As<IUserSessionService>().SingleInstance();

            builder.RegisterType<Engine>().As<IEngine>().SingleInstance();

            //services
            builder.RegisterType<ProductService>().As<IProductService>().SingleInstance();
            builder.RegisterType<CategoryService>().As<ICategoryService>().SingleInstance();

            builder.RegisterType<UserService>().AsSelf().SingleInstance();




            //Commands
            builder.RegisterType<AddProductToProductsCommand>().Named<ICommand>("addProduct");
            builder.RegisterType<ImportCouriersCommand>().Named<ICommand>("importCouriers");
            builder.RegisterType<ImportProductsCommand>().Named<ICommand>("importProducts");
            builder.RegisterType<ImportSuppliersCommand>().Named<ICommand>("importSuppliers");
            builder.RegisterType<RemoveProductFromProductsCommand>().Named<ICommand>("removeProduct");



            
            
            builder.RegisterType<ExitCommand>().Named<ICommand>("exit");
            builder.RegisterType<LoginCommand>().Named<ICommand>("login");
            builder.RegisterType<LogoutCommand>().Named<ICommand>("logout");
            builder.RegisterType<PrintAvailableProductReportCommand>().Named<ICommand>("reportProducts");
            builder.RegisterType<PrintOrdersReportCommand>().Named<ICommand>("reportOrders");
            builder.RegisterType<RegisterUserCommand>().Named<ICommand>("register");


            builder.RegisterType<SearchCategoryCommand>().Named<ICommand>("searchByCategory");
            builder.RegisterType<SearchProductCommand>().Named<ICommand>("searchByName");
           








        }
    }
}
