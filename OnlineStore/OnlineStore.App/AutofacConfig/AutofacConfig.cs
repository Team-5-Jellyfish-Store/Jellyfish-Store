using Autofac;
using AutoMapper;
using OnlineStore.Core;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Commands.AdminCommands;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Factories;
using OnlineStore.Core.Providers;
using OnlineStore.Data;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic;
using OnlineStore.Logic.Contracts;
using OnlineStore.Logic.Services;

namespace OnlineStore.App.AutofacConfig
{
    internal class AutofacConfig : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandFactory>().As<ICommandFactory>();
            builder.RegisterType<CommandParser>().As<ICommandParser>();
            builder.RegisterType<CommandProcessor>().As<ICommandProcessor>();
            builder.RegisterType<ConsoleReader>().As<IReader>();
            builder.RegisterType<ConsoleWriter>().As<IWriter>();
            builder.RegisterType<Hasher>().As<IHasher>();
            builder.RegisterType<Validator>().As<IValidator>();

            builder.RegisterType<OnlineStoreContext>().As<IOnlineStoreContext>().InstancePerLifetimeScope();
            builder.RegisterType<UserSession>().As<IUserSession>().SingleInstance();

            builder.RegisterType<Engine>().As<IEngine>().SingleInstance();

            //services
            builder.RegisterType<ProductService>().As<IProductService>().SingleInstance();
            builder.RegisterType<CategoryService>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<SupplierService>().As<ISupplierService>().SingleInstance();
            builder.RegisterType<ImportService>().As<IImportService>().SingleInstance();
            builder.RegisterType<CourierService>().As<ICourierService>().SingleInstance();
            builder.RegisterType<AddressService>().As<IAddressService>().SingleInstance();
            builder.RegisterType<TownService>().As<ITownService>().SingleInstance();

            builder.RegisterType<UserService>().AsSelf().SingleInstance();
            builder.Register(x => Mapper.Instance);

            //Commands
            builder.RegisterType<AddProductToProductsCommand>().Named<ICommand>("addProduct");
            builder.RegisterType<ImportCouriersCommand>().Named<ICommand>("importCouriers");
            builder.RegisterType<ImportProductsCommand>().Named<ICommand>("importProducts");
            builder.RegisterType<ImportSuppliersCommand>().Named<ICommand>("importSuppliers");
            builder.RegisterType<ImportExternalDataCommand>().Named<ICommand>("import");
            builder.RegisterType<RemoveProductFromProductsCommand>().Named<ICommand>("removeProduct");

            builder.RegisterType<ExitCommand>().Named<ICommand>("exit");
            builder.RegisterType<LoginCommand>().Named<ICommand>("login");
            builder.RegisterType<LogoutCommand>().Named<ICommand>("logout");
            builder.RegisterType<RegisterUserCommand>().Named<ICommand>("register");
            builder.RegisterType<PrintAvailableProductReportCommand>().Named<ICommand>("reportProducts");
            builder.RegisterType<PrintOrdersReportCommand>().Named<ICommand>("reportOrders");
            builder.RegisterType<AddOrderCommand>().Named<ICommand>("addOrder");

            builder.RegisterType<SearchCategoryCommand>().Named<ICommand>("searchByCategory");
            builder.RegisterType<SearchProductCommand>().Named<ICommand>("searchByName");

            //Services
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<AddressService>().As<IAddressService>().SingleInstance();
            builder.RegisterType<OrderService>().As<IOrderService>().SingleInstance();

            //builder.RegisterType<Mapper>().As<IMapper>();
            builder.Register(x => Mapper.Instance).SingleInstance();
        }
    }
}
