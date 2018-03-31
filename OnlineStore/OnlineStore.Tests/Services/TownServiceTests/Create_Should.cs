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

namespace OnlineStore.Tests.Services.TownServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void Throw_WhenStringEmpty()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var fakeTownService = new TownService(stubDBContext.Object);
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => fakeTownService.Create(null));

        }

        [TestMethod]
        public void Throw_WhenTownExists()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var fakeTownService = new TownService(stubDBContext.Object);
            var fakeTown = new Town();
            fakeTown.Name = "test";

            var data = new List<Town> { fakeTown };
            var stubDBSet = new Mock<DbSet<Town>>();

            stubDBSet.SetupData(data);
            stubDBContext.Setup(x => x.Towns).Returns(stubDBSet.Object);
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => fakeTownService.Create(fakeTown.Name));

        }

        [TestMethod]
        public void CalledTownAdd_WhenValidData()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var fakeTownService = new TownService(stubDBContext.Object);

            var data = new List<Town>();
            var mockDBSet = new Mock<DbSet<Town>>();
            mockDBSet.SetupData(data);
            stubDBContext.Setup(x => x.Towns).Returns(mockDBSet.Object);

            //WHY DOESN"T WORK?
            //stubDBContext.Setup(x => x.Towns.Add(It.IsAny<Town>())).Verifiable();
            mockDBSet.Setup(x => x.Add(It.IsAny<Town>())).Verifiable();

            //Act
            fakeTownService.Create("test");
            //Assert

            //WHY DOESN"T WORK?
            //stubDBContext.Verify(x => x.Towns.Add(It.IsAny<Town>()), Times.Once);
            mockDBSet.Verify(x => x.Add(It.IsAny<Town>()), Times.Once);
        }

        [TestMethod]
        public void CalledSaveChanges_WhenValidData()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var fakeTownService = new TownService(stubDBContext.Object);

            var data = new List<Town>();
            var stubDBSet = new Mock<DbSet<Town>>();
            stubDBSet.SetupData(data);
            stubDBContext.Setup(x => x.Towns).Returns(stubDBSet.Object);

            //Act
            fakeTownService.Create("test");

            //Assert
            stubDBContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
