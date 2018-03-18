using AutoMapper;
using OnlineStore.Core.DTO;
using OnlineStore.Models;

namespace OnlineStore.Core.AutomapperConfig
{
    public static class AutomapperConfiguration
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CourierDto, Courier>().ReverseMap();
            });
        }
    }
}
