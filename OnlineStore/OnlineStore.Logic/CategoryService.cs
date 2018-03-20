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

        public CategoryService(IOnlineStoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<CategoryModel> GetAllCategories()
        {
            return context.Categories.ProjectTo<CategoryModel>();
        }

        public int FindIdByName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException();
            }
            var categoryFound = this.context.Categories.FirstOrDefault(f => f.Name == name) ?? throw new ArgumentException("Category not found!");
            return categoryFound.Id;
        }

        public void Create(string name)
        {
            var categoryToAdd = new Category() {Name = name};
            this.context.Categories.Add(categoryToAdd);
            this.context.SaveChanges();
        }
    }
}
