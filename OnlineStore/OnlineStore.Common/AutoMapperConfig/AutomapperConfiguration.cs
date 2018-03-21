using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using OnlineStore.DTO.Mapping;

namespace OnlineStore.Common.AutoMapperConfig
{
    public static class AutomapperConfiguration
    {
        //public static void Initialize()
        //{
        //    Mapper.Initialize(cfg =>
        //    {
        //      cfg.CreateMap<ProductImportModel, Product>().ForMember(x => x.SellingPrice, confg => confg.MapFrom(x => x.PurchasePrice * 1.5m));
        //        cfg.CreateMap<TownImportModel, Town>().ReverseMap();
        //        cfg.CreateMap<UserRegisterModel, User>().ReverseMap();
        //        cfg.CreateMap<AddressModel, Address>().ReverseMap();
        //        cfg.CreateMap<OrderModel, Order>().ReverseMap();
        //        cfg.CreateMap<TownModel, Town>().ReverseMap();

        //    });
        //}
        public static void Initialize()
        {
            var types = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(x => !x.IsDynamic)
                .SelectMany(x => x.GetReferencedAssemblies())
                .Select(x => Assembly.Load(x))
                .SelectMany(x => x.GetTypes());

            Mapper.Initialize(cfg => Load(types, cfg));
        }


        private static void Load(IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            LoadStandardMappings(types, cfg);
            LoadCustomMappings(types, cfg);
        }

        private static void LoadStandardMappings(IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            LoadMapFrom(types, cfg);
            LoadMapTo(types, cfg);
        }

        private static void LoadMapTo(IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            var typesFoundForMapping = types
                  .Where(t => !t.IsInterface && !t.IsAbstract)
                  .Where(t => t.GetInterfaces()
                          .Any(i => i.IsGenericType &&
                               i.GetGenericTypeDefinition().Equals(typeof(IMapTo<>)))).ToList().Select(x => new
                               {
                                   source = x,
                                   dest = x.GetInterfaces().FirstOrDefault(y => y.GetGenericTypeDefinition() == typeof(IMapTo<>)).GetGenericArguments()[0]
                               });

            foreach (var type in typesFoundForMapping)
            {
                cfg.CreateMap(type.source, type.dest).ReverseMap();
            }
        }

        private static void LoadMapFrom(IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            var typesFoundForMapping = types
                  .Where(t => !t.IsInterface && !t.IsAbstract)
                  .Where(t => t.GetInterfaces()
                          .Any(i => i.IsGenericType &&
                               i.GetGenericTypeDefinition().Equals(typeof(IMapFrom<>)))).ToList().Select(x => new
                               {
                                   source = x,
                                   dest = x.GetInterfaces().FirstOrDefault(y => y.GetGenericTypeDefinition() == typeof(IMapFrom<>)).GetGenericArguments()[0]
                               });

            foreach (var type in typesFoundForMapping)
            {
                cfg.CreateMap(type.source, type.dest).ReverseMap();
            }
        }

        private static void LoadCustomMappings(IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            var typesFoundForMapping = types
                  .Where(t => !t.IsInterface && !t.IsAbstract)
                  .Where(t => t.GetInterfaces().Any(i => typeof(IHaveCustomMappings).IsAssignableFrom(i)))
                          .Select(x => (IHaveCustomMappings)Activator.CreateInstance(x)).ToArray();


            foreach (var type in typesFoundForMapping)
            {
                type.CreateMappings(cfg);
            }
        }
    }
}
