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
using OnlineStore.DTO.CategoryModels;
using OnlineStore.DTO.CourierModels;
using OnlineStore.DTO.Factory;
using OnlineStore.DTO.OrderModels;
using OnlineStore.DTO.OrderModels.Constracts;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.ProductModels.Contracts;
using OnlineStore.DTO.SupplierModels;
using OnlineStore.DTO.UserModels;
using OnlineStore.DTO.UserModels.Contracts;
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
            builder.RegisterType<PDFExporter>().As<IPDFExporter>();

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
            builder.RegisterType<JsonService>().As<IJsonService>();
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
            builder.RegisterType<SearchCategoryCommand>().Named<ICommand>("searchbycategory");
            builder.RegisterType<SearchProductCommand>().Named<ICommand>("searchbyname");
            builder.RegisterType<HelpCommand>().Named<ICommand>("help");

            //DTOs
            builder.RegisterType<CategoryModel>().As<ICategoryModel>();
            builder.RegisterType<CourierImportModel>().As<ICourierImportModel>();
            builder.RegisterType<OrderModel>().As<IOrderModel>();
            builder.RegisterType<OrderMakeModel>().As<IOrderMakeModel>();
            builder.RegisterType<ProductImportModel>().As<IProductImportModel>();
            builder.RegisterType<ProductModel>().As<IProductModel>();
            builder.RegisterType<SuppliersImportModel>().As<ISuppliersImportModel>();
            builder.RegisterType<UserLoginModel>().As<IUserLoginModel>();
            builder.RegisterType<UserRegisterModel>().As<IUserRegisterModel>();
        }
    }
}
