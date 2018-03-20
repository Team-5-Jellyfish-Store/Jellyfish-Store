using AutoMapper.QueryableExtensions;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Logic
{
    public class CategoryService : ICategoryService
    {
        private readonly IOnlineStoreContext context;

        public CategoryService(IOnlineStoreContext context)
        {
            this.context = context;
        }


        public IEnumerable<CategoryModel> GetAllCategories()
        {
            return context.Categories.ProjectTo<CategoryModel>();
        }
    }
}
