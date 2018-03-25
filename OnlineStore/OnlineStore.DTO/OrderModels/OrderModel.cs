﻿using OnlineStore.DTO.MappingContracts;
using OnlineStore.Models.DataModels;
using System;
using AutoMapper;

namespace OnlineStore.DTO.OrderModels
{
    public class OrderModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int ProductsCount { get; set; }

        public string Comment { get; set; }

        public DateTime OrderedOn { get; set; }

        public Nullable<DateTime> DeliveredOn { get; set; }

        public string Username { get; set; } //navprop

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Order, OrderModel>().ForMember(x => x.Username, cfg => cfg.MapFrom(x => x.User.Username));
        }
    }
}
