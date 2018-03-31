using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Common.AutoMapperConfig;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO.CategoryModels;
using OnlineStore.Logic.Services;
using OnlineStore.Models.DataModels;
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
    public class FindCategoryByName
    {
        [TestMethod]
        public void Throw_WhenStringEmpty()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var stubMapper = new Mock<IMapper>();
            var fakeCategoryService = new CategoryService(stubDBContext.Object, stubMapper.Object);
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => fakeCategoryService.FindCategoryByName(""));

        }

        [TestMethod]
        public void Throw_CategoryFoundIsNull()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var stubMapper = new Mock<IMapper>();
            var fakeCategoryService = new CategoryService(stubDBContext.Object, stubMapper.Object);

            var data = new List<Category>();
            
            var stubDbSet = new Mock<DbSet<Category>>();
            stubDbSet.SetupData(data);
            stubDBContext.Setup(x => x.Categories).Returns(stubDbSet.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => fakeCategoryService.FindCategoryByName("test"));
        }

        [TestMethod]
        public void Returns_CorrectCategoryDTO()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var stubMapper = new Mock<IMapper>();
            var fakeCategoryService = new CategoryService(stubDBContext.Object, stubMapper.Object);
            var fakeCategoryModel = new Mock<ICategoryModel>();
            
            var data = new List<Category>
            {
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

            stubMapper.Setup(x => x.Map<ICategoryModel>(It.IsAny<Category>())).Returns(fakeCategoryModel.Object);
            //Act 
            var foundCategory =  fakeCategoryService.FindCategoryByName("test");

            //Assert
            Assert.IsInstanceOfType(foundCategory,typeof(ICategoryModel));
        }

        [TestMethod]
        public void CategoryDTO_IsInstanceOfCorrectType()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var stubMapper = new Mock<IMapper>();
            var fakeCategoryService = new CategoryService(stubDBContext.Object, stubMapper.Object);
            var expected = new CategoryModel { Name = "test" };
            var data = new List<Category>
            {
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

            stubMapper.Setup(x => x.Map<ICategoryModel>(It.IsAny<Category>())).Returns(expected);
            //Act 
            var foundCategory = fakeCategoryService.FindCategoryByName("test");

            //Assert
            Assert.AreEqual(expected, foundCategory);
        }


    }
}
