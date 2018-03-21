using OnlineStore.DTO.Mapping;
using OnlineStore.Models.DataModels;

namespace OnlineStore.DTO
{
    public class AddressModel : IMapTo<Address>
    {
        public string AddressText { get; set; }
    }
}
