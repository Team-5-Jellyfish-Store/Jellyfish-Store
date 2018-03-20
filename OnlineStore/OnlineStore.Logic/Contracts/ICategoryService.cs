using OnlineStore.DTO;
using System.Collections.Generic;

namespace OnlineStore.Logic.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<CategoryModel> GetAllCategories();
        int FindIdByName(string name);
        void Create(string name);
    }
}
