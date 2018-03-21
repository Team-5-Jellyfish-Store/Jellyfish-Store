using AutoMapper;
using OnlineStore.DTO.Mapping;
using OnlineStore.Models.DataModels;

namespace OnlineStore.DTO
{
    public class AddressImportModel : IMapFrom<Address>//, IHaveCustomMappings
    {
       public string AddressText { get; set; }

       public string TownName { get; set; }

        //public void CreateMappings(IMapperConfigurationExpression configuration)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
