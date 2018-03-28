using OnlineStore.DTO.MappingContracts;
using OnlineStore.Models.DataModels;

namespace OnlineStore.DTO.CategoryModels
{
    public class CategoryModel : IMapFrom<Category>, ICategoryModel
    {
        public string Name { get; set; }
    }
}
