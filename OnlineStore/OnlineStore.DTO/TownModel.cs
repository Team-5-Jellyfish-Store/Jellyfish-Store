using OnlineStore.DTO.Mapping;
using OnlineStore.Models.DataModels;

namespace OnlineStore.DTO
{
    public class TownModel : IMapTo<Town>
    {
        public string Name { get; set; }
    }
}