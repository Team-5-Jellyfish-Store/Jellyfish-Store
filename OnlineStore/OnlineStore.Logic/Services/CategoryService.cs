using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStore.Models.DataModels;
using OnlineStore.DTO.CategoryModels;

namespace OnlineStore.Logic.Services
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

        public CategoryModel FindCategoryByName(string name)
        {
            var category = context.Categories.FirstOrDefault(x => x.Name == name);
            if (category == null)
            {
                throw new ArgumentNullException("No such category!");

            }

            var categoryModel = this.mapper.Map<CategoryModel>(category);
            return categoryModel;
        }

        public void Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Category name is required!", nameof(name));
            }

            if (this.context.Categories.Any(x => x.Name == name))
            {
                throw new ArgumentException($"Category {name} already exists!");
            }

            var categoryToAdd = new Category { Name = name };

            this.context.Categories.Add(categoryToAdd);
            this.context.SaveChanges();
        }

        public IEnumerable<CategoryModel> GetAllCategories()
        {
            return context.Categories.ProjectTo<CategoryModel>();
        }
    }
}
