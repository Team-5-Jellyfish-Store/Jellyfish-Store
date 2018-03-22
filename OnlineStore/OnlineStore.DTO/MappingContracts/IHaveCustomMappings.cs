using AutoMapper;

namespace OnlineStore.DTO.MappingContracts
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
