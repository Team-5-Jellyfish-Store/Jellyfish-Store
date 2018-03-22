using OnlineStore.DTO.MappingContracts;
using OnlineStore.Models.DataModels;

namespace OnlineStore.DTO
{
    public class AddressModel : IMapTo<Address>
    {
        public string AddressAddressText { get; set; }
    }
}
