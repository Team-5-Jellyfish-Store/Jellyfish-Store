using OnlineStore.DTO;
using System.Collections.Generic;
using OnlineStore.Models.DataModels;

namespace OnlineStore.Logic.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<CategoryModel> GetAllCategories();

        CategoryModel FindCategoryByName(string name);

        // void RemoveCategoryByName(string name);
        int GetIdByName(string name);
        //void Create(string name);

        Category FindOrCreate(string name);

        Category Create(string name);
    }
}
