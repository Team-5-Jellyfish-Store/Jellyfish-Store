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

namespace OnlineStore.Tests.Services.AdressServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void Throw_WhenAdressTextIsNull()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var fakeTownName = "test";
            var fakeAdressService = new AddressService(stubDBContext.Object);

            //Act&Assert
            Assert.ThrowsException<ArgumentException>(() => fakeAdressService.Create(null, fakeTownName));
        }

        [TestMethod]
        public void Throw_WhenTownNameIsNull()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var fakeAdressText = "test";
            var fakeAdressService = new AddressService(stubDBContext.Object);

            //Act&Assert
            Assert.ThrowsException<ArgumentException>(() => fakeAdressService.Create(fakeAdressText, null));
        }

        [TestMethod]
        public void Throw_IfTownNameIsNull()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var fakeAdressText = "test";
            var fakeAdressService = new AddressService(stubDBContext.Object);

            //Act&Assert
            Assert.ThrowsException<ArgumentException>(() => fakeAdressService.Create(fakeAdressText, null));
        }

        [TestMethod]
        public void Throw_IfTownDoesntExist()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var fakeAdressText = "adress";
            var fakeTownName = "town";

            var fakeAdressService = new AddressService(stubDBContext.Object);
            var stubDBSet = new Mock<DbSet<Town>>();
            var data = new List<Town>();
            stubDBSet.SetupData(data);
            stubDBContext.Setup(x => x.Towns).Returns(stubDBSet.Object);
            //Act
            //fakeAdressService.Create(fakeAdressText, fakeTownName);
            //Assert
            Assert.ThrowsException<ArgumentException>(() => fakeAdressService.Create(fakeAdressText, fakeTownName));

        }

        [TestMethod]
        public void Throw_IfThereIsSuchAddressWhoHasTheSameTown()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var fakeAdressText = "adress";
            var fakeTownName = "town";
            var fakeAdressService = new AddressService(stubDBContext.Object);

            var fakeTown = new Town { Name = fakeTownName };
            var stubDBSetTowns = new Mock<DbSet<Town>>();
            var dataTowns = new List<Town> { fakeTown };
            stubDBSetTowns.SetupData(dataTowns);
            stubDBContext.Setup(x => x.Towns).Returns(stubDBSetTowns.Object);

            var fakeAddress = new Address { AddressText = fakeAdressText, Town = fakeTown };
            var stubDBSetAddresses = new Mock<DbSet<Address>>();
            var dataAddresses = new List<Address> { fakeAddress };
            stubDBSetAddresses.SetupData(dataAddresses);
            stubDBContext.Setup(x => x.Addresses).Returns(stubDBSetAddresses.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => fakeAdressService.Create(fakeAdressText, fakeTownName));
        }

        [TestMethod]
        public void AddsAdress_IfEverythingValid()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var fakeAdressText = "adress";
            var fakeTownName = "town";
            var fakeAdressService = new AddressService(stubDBContext.Object);

            var fakeTown = new Town { Name = fakeTownName };
            var stubDBSetTowns = new Mock<DbSet<Town>>();
            var dataTowns = new List<Town> { fakeTown };
            stubDBSetTowns.SetupData(dataTowns);
            stubDBContext.Setup(x => x.Towns).Returns(stubDBSetTowns.Object);

            var mockDBSetAddresses = new Mock<DbSet<Address>>();
            var dataAddresses = new List<Address>();
            mockDBSetAddresses.SetupData(dataAddresses);
            stubDBContext.Setup(x => x.Addresses).Returns(mockDBSetAddresses.Object);
            mockDBSetAddresses.Setup(x => x.Add(It.IsAny<Address>())).Verifiable();
            //Act
            fakeAdressService.Create(fakeAdressText, fakeTownName);

            //Assert
            mockDBSetAddresses.Verify(x => x.Add(It.IsAny<Address>()), Times.Once);
        }

        [TestMethod]
        public void CallSaveChanges_IfEverythingValid()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            var fakeAdressText = "adress";
            var fakeTownName = "town";
            var fakeAdressService = new AddressService(stubDBContext.Object);

            var fakeTown = new Town { Name = fakeTownName };
            var stubDBSetTowns = new Mock<DbSet<Town>>();
            var dataTowns = new List<Town> { fakeTown };
            stubDBSetTowns.SetupData(dataTowns);
            stubDBContext.Setup(x => x.Towns).Returns(stubDBSetTowns.Object);

            var stubDBSetAddresses = new Mock<DbSet<Address>>();
            var dataAddresses = new List<Address>();
            stubDBSetAddresses.SetupData(dataAddresses);
            stubDBContext.Setup(x => x.Addresses).Returns(stubDBSetAddresses.Object);
            //Act
            fakeAdressService.Create(fakeAdressText, fakeTownName);

            //Assert
            stubDBContext.Verify(x => x.SaveChanges(), Times.Once);
        }



    }
}
