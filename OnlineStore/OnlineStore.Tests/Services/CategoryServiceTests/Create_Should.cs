using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic.Services;
using OnlineStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Tests.Services.CategoryServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void Throw_WhenStringEmpty()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var stubMapper = new Mock<IMapper>();
            var fakeCategoryService = new CategoryService(stubDBContext.Object, stubMapper.Object);
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => fakeCategoryService.Create(null));

        }

        [TestMethod]
        public void Throw_WhenCategoryExists()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var stubMapper = new Mock<IMapper>();
            var fakeCategoryService = new CategoryService(stubDBContext.Object, stubMapper.Object);
            var fakeCategory = new Category();
            fakeCategory.Name = "test";

            var data = new List<Category> { fakeCategory };
            var stubDBSet = new Mock<DbSet<Category>>();
            stubDBSet.SetupData(data);
            stubDBContext.Setup(x => x.Categories).Returns(stubDBSet.Object);
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => fakeCategoryService.Create(fakeCategory.Name));

        }

        [TestMethod]
        public void CalledCategoryAdd_WhenValidData()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var stubMapper = new Mock<IMapper>();
            var fakeCategoryService = new CategoryService(stubDBContext.Object, stubMapper.Object);

            var data = new List<Category>();
            var mockDBSet = new Mock<DbSet<Category>>();
            mockDBSet.SetupData(data);
            stubDBContext.Setup(x => x.Categories).Returns(mockDBSet.Object);

            //WHY DOESN"T WORK?
            //stubDBContext.Setup(x => x.Categories.Add(It.IsAny<Category>())).Verifiable();
            mockDBSet.Setup(x => x.Add(It.IsAny<Category>())).Verifiable();

            //Act
            fakeCategoryService.Create("test");
            //Assert

            //WHY DOESN"T WORK?
            //stubDBContext.Verify(x => x.Categories.Add(It.IsAny<Category>()), Times.Once);
            mockDBSet.Verify(x => x.Add(It.IsAny<Category>()), Times.Once);
        }

        [TestMethod]
        public void CalledSaveChanges_WhenValidData()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var stubMapper = new Mock<IMapper>();
            var fakeCategoryService = new CategoryService(stubDBContext.Object, stubMapper.Object);

            var data = new List<Category>();
            var stubDBSet = new Mock<DbSet<Category>>();
            stubDBSet.SetupData(data);
            stubDBContext.Setup(x => x.Categories).Returns(stubDBSet.Object);

            //Act
            fakeCategoryService.Create("test");

            //Assert
            stubDBContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
