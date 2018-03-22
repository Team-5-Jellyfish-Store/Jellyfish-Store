using OnlineStore.DTO;
using System.Collections.Generic;

namespace OnlineStore.Logic.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<CategoryModel> GetAllCategories();

        CategoryModel FindCategoryByName(string name);

        // void RemoveCategoryByName(string name);
        int GetIdByName(string name);
        void Create(string name);
    }
}
