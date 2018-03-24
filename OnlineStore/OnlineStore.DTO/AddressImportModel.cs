using AutoMapper;
using OnlineStore.DTO.MappingContracts;
using OnlineStore.Models.DataModels;

namespace OnlineStore.DTO
{
    public class AddressImportModel : IMapFrom<Address>
    {
        public string AddressText { get; set; }

        public string TownName { get; set; }
    }
}
