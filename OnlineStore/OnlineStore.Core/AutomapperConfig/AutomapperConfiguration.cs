using AutoMapper;
using OnlineStore.Core.DTO;
using OnlineStore.DTO;
using OnlineStore.Models.DataModels;

namespace OnlineStore.Core.AutomapperConfig
{
    public static class AutomapperConfiguration
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CourierImportDto, Courier>().ReverseMap();
                cfg.CreateMap<ProductModel ,Product>().ReverseMap();
                cfg.CreateMap<CategoryModel, Category>().ReverseMap();


            });
        }
    }
}
