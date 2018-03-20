using OnlineStore.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Logic.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<CategoryModel> GetAllCategories();
    }
}
