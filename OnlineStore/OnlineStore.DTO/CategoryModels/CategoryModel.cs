using OnlineStore.DTO.MappingContracts;
using OnlineStore.Models.DataModels;

namespace OnlineStore.DTO.CategoryModels
{
    public class CategoryModel : ICategoryModel, IMapFrom<Category>
    {
        public string Name { get; set; }
    }
}
