using Autofac;
using AutoMapper;
using OnlineStore.Core;
using OnlineStore.Core.Commands;
using OnlineStore.Core.Commands.AdminCommands;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Factories;
using OnlineStore.Core.Providers.Providers;
using OnlineStore.Data;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO.Factory;
using OnlineStore.Logic.Contracts;
using OnlineStore.Logic.Services;
using OnlineStore.Providers.Contracts;
using OnlineStore.Providers.Providers;

namespace OnlineStore.App.AutofacConfig
{
    public class AutofacConfiguration : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandFactory>().As<ICommandFactory>();
            builder.RegisterType<CommandParser>().As<ICommandParser>();
            builder.RegisterType<CommandProcessor>().As<ICommandProcessor>();
            builder.RegisterType<DataTransferObjectFactory>().As<IDataTransferObjectFactory>();
            builder.RegisterType<FileReader>().As<IFileReader>();
            builder.RegisterType<ConsoleReader>().As<IReader>();
            builder.RegisterType<ConsoleWriter>().As<IWriter>();
            builder.RegisterType<Hasher>().As<IHasher>();
            builder.RegisterType<Validator>().As<IValidator>();
            builder.RegisterType<DatetimeProvider>().AsSelf();

            builder.RegisterType<OnlineStoreContext>().As<IOnlineStoreContext>().InstancePerLifetimeScope();
            builder.RegisterType<UserSession>().As<IUserSession>().SingleInstance();

            builder.RegisterType<Engine>().As<IEngine>().SingleInstance();

            //services
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<SupplierService>().As<ISupplierService>();
            builder.RegisterType<ImportService>().As<IImportService>();
            builder.RegisterType<CourierService>().As<ICourierService>();
            builder.RegisterType<AddressService>().As<IAddressService>();
            builder.RegisterType<TownService>().As<ITownService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.Register(x => Mapper.Instance);

            //Commands
            builder.RegisterType<AddProductToProductsCommand>().Named<ICommand>("addproduct");
            builder.RegisterType<ImportExternalDataCommand>().Named<ICommand>("importexternaldata");
            builder.RegisterType<RemoveProductFromProductsCommand>().Named<ICommand>("removeproduct");
            builder.RegisterType<LoginCommand>().Named<ICommand>("login");
            builder.RegisterType<LogoutCommand>().Named<ICommand>("logout");
            builder.RegisterType<RegisterUserCommand>().Named<ICommand>("register");
            builder.RegisterType<PrintAvailableProductReportCommand>().Named<ICommand>("reportproducts");
            builder.RegisterType<PrintOrdersReportCommand>().Named<ICommand>("reportorders");
            builder.RegisterType<AddOrderCommand>().Named<ICommand>("addorder");

            builder.RegisterType<SearchCategoryCommand>().Named<ICommand>("searchByCategory");
            builder.RegisterType<SearchProductCommand>().Named<ICommand>("searchByName");
        }
    }
}
