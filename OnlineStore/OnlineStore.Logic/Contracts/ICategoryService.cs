using OnlineStore.DTO;
using System.Collections.Generic;
using OnlineStore.Models.DataModels;
using OnlineStore.DTO.CategoryModels;

namespace OnlineStore.Logic.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<ICategoryModel> GetAllCategories();

        ICategoryModel FindCategoryByName(string name);
        
        void Create(string name);
    }
}
