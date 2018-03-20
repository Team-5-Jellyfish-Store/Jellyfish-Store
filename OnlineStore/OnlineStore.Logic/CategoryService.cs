using AutoMapper;
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
        private readonly IMapper mapper;

        public CategoryService(IOnlineStoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void Create(string name)
        {
            throw new NotImplementedException();
        }

        public CategoryModel FindCategoryByName(string name)
        {
            var category = context.Categories.FirstOrDefault(x => x.Name == name);
            if (category == null)
            {
                throw new ArgumentNullException("No such category!");

            }

            var categoryModel = mapper.Map<CategoryModel>(category);
            return categoryModel;
        }

        public int FindIdByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryModel> GetAllCategories()
        {
            return context.Categories.ProjectTo<CategoryModel>();
        }
    }
}
