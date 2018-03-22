using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStore.Models.DataModels;

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
            var categoryToAdd = new Category() { Name = name };
            this.context.Categories.Add(categoryToAdd);
            this.context.SaveChanges();
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

        public int GetIdByName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException();
            }
            var categoryFound = this.context.Categories.FirstOrDefault(f => f.Name == name) ?? throw new ArgumentException("Category not found!");
            return categoryFound.Id;
        }

        public IEnumerable<CategoryModel> GetAllCategories()
        {
            return context.Categories.ProjectTo<CategoryModel>();
        }
    }
}
