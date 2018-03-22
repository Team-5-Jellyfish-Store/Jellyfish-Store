using OnlineStore.DTO.MappingContracts;
using OnlineStore.Models.DataModels;

namespace OnlineStore.DTO
{
    public class CategoryModel : IMapTo<Category>
    {
        public string Name { get; set; }
    }
}
