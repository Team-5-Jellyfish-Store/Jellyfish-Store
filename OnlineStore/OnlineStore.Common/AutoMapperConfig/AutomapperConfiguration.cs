using AutoMapper;
using OnlineStore.Models.DataModels;
using OnlineStore.DTO;
namespace OnlineStore.Common.AutoMapperConfig
{
    public static class AutomapperConfiguration
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
              cfg.CreateMap<ProductImportModel, Product>().ForMember(x => x.SellingPrice, confg => confg.MapFrom(x => x.PurchasePrice * 1.5m));
                cfg.CreateMap<TownImportModel, Town>().ReverseMap();
                cfg.CreateMap<UserRegisterModel, User>().ReverseMap();
                cfg.CreateMap<AddressModel, Address>().ReverseMap();
                cfg.CreateMap<OrderModel, Order>().ReverseMap();
                cfg.CreateMap<TownModel, Town>().ReverseMap();

            });
        }
    }
}
