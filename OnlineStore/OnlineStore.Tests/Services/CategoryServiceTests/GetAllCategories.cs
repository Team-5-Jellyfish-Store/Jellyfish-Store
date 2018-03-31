using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Common.AutoMapperConfig;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic.Services;
using OnlineStore.Models.DataModels;
using OnlineStore.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Tests.Services.CategoryServiceTests
{
    [TestClass]
    public class GetAllCategories
    {
        [TestMethod]
        public void ReturnAllCategoriesCorrect()
        {
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var stubMapper = new Mock<IMapper>();
            var fakeCategoryService = new CategoryService(stubDBContext.Object, stubMapper.Object);
            var data = new List<Category>
            {
                new Category
                {
                Id = 1,
                Name = "test",
                Products = new Collection<Product>()
                },
                new Category
                {
                Id = 1,
                Name = "test",
                Products = new Collection<Product>()
                },
                new Category
                {
                Id = 1,
                Name = "test",
                Products = new Collection<Product>()
                }
            };
            var stubDbSet = new Mock<DbSet<Category>>();
            stubDbSet.SetupData(data);
            stubDBContext.Setup(x => x.Categories).Returns(stubDbSet.Object);

            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            //Act 
            var foundCategories = fakeCategoryService.GetAllCategories();

            //Assert
            Assert.AreEqual(data.Count, foundCategories.Count());
        }
    }
}
