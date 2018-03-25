using OnlineStore.DTO;
using System.Collections.Generic;
using OnlineStore.Models.DataModels;
using OnlineStore.DTO.CategoryModels;

namespace OnlineStore.Logic.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<CategoryModel> GetAllCategories();

        CategoryModel FindCategoryByName(string name);

        int GetIdByName(string name);

        void Create(string name);
    }
}
