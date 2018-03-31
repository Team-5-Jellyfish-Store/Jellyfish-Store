using OnlineStore.DTO.MappingContracts;
using OnlineStore.Models.DataModels;
using System;
using AutoMapper;
using OnlineStore.DTO.OrderModels.Constracts;

namespace OnlineStore.DTO.OrderModels
{
    public class OrderModel : IMapFrom<Order>, IHaveCustomMappings, IOrderModel
    {
        public string Comment { get; set; }

        public DateTime OrderedOn { get; set; }

        public DateTime? DeliveredOn { get; set; }

        public string Username { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //If the line under is deleted test is OK :D
            configuration.CreateMap<Order, OrderModel>().ForMember(x => x.Username, cfg => cfg.MapFrom(x => x.User.Username));
        }
    }
}

