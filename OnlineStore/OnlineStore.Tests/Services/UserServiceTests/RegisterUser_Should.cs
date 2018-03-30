using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO.UserModels;
using OnlineStore.Logic.Contracts;
using OnlineStore.Logic.Services;
using OnlineStore.Models.DataModels;
using OnlineStore.Tests.Mocks;
using System;
using System.Collections.Generic;

namespace OnlineStore.Tests.Services.UserServiceTests
{
    [TestClass]
    public class RegisterUser_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_UserRegisterModel_IsNull()
        {
            // Arrange
            var ctxMock = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxMock.Object, mapperStub.Object, townServiceStub.Object, addressServiceStub.Object);

            Action executingRegisterUserMethod = () => userService.RegisterUser(null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(executingRegisterUserMethod);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Username_AlreadyExists_InDatabase()
        {
            // Arrange
            var username = "testUsername";
            var userRegisterModel = new UserRegisterModel() { Username = username };

            var users = new List<User>() { new User() { Username = username } };
            var usersMock = users.GetQueryableMockDbSet<User>();

            var ctxMock = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxMock.Object, mapperStub.Object, townServiceStub.Object, addressServiceStub.Object);

            ctxMock
                .Setup(x => x.Users)
                .Returns(usersMock.Object);

            Action executingRegisterUserMethod = () => userService.RegisterUser(userRegisterModel);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingRegisterUserMethod);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Email_AlreadyExists_InDatabase()
        {
            // Arrange
            var username = "testUsername";
            var email = "test@email";
            var userRegisterModel = new UserRegisterModel()
            {
                Username = username,
                EMail = email
            };

            var users = new List<User>() { new User() { EMail = email } };
            var usersMock = users.GetQueryableMockDbSet<User>();

            var ctxMock = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxMock.Object, mapperStub.Object, townServiceStub.Object, addressServiceStub.Object);

            ctxMock
                .Setup(x => x.Users)
                .Returns(usersMock.Object);

            Action executingRegisterUserMethod = () => userService.RegisterUser(userRegisterModel);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingRegisterUserMethod);
        }

        [TestMethod]
        public void Invoke_TownServiceCreate_When_TownName_DoesNotExists_InDatabase()
        {
            // Arrange
            var username = "testUsername";
            var email = "test@email";
            var townName = "testTownName";
            var addressText = "testAdress";
            var userRegisterModel = new UserRegisterModel()
            {
                Username = username,
                EMail = email,
                TownName = townName,
                AddressText = addressText
            };

            var townToAdd = new Town() { Name = townName };
            var addressToAdd = new Address() { AddressText = addressText };

            var usersMock = new List<User>() { }.GetQueryableMockDbSet();
            var townsMock = new List<Town> { }.GetQueryableMockDbSet();

            var userToRegister = new User();

            var ctxMock = new Mock<IOnlineStoreContext>();
            var mapperMock = new Mock<IMapper>();
            var townServiceMock = new Mock<ITownService>();
            var addressServiceMock = new Mock<IAddressService>();

            var userService = new UserService(ctxMock.Object, mapperMock.Object, townServiceMock.Object, addressServiceMock.Object);

            Action addingTownToTownsMock =
                () =>
                    ctxMock
                        .Setup(x => x.Towns)
                        .Returns((townsMock = new List<Town> { townToAdd }.GetQueryableMockDbSet()).Object);

            Action addingAddressToTown =
                () => townToAdd.Addresses.Add(addressToAdd);

            ctxMock
                .Setup(x => x.Users)
                .Returns(usersMock.Object);

            ctxMock
                .Setup(x => x.Towns)
                .Returns(townsMock.Object);

            townServiceMock
                .Setup(ts => ts.Create(userRegisterModel.TownName))
                .Callback(addingTownToTownsMock);

            addressServiceMock
                .Setup(addServ => addServ.Create(userRegisterModel.AddressText, userRegisterModel.TownName))
                .Callback(addingAddressToTown);

            mapperMock
                .Setup(m => m.Map<User>(userRegisterModel))
                .Returns(userToRegister);

            // Act
            userService.RegisterUser(userRegisterModel);

            // Assert
            townServiceMock.Verify(ts => ts.Create(userRegisterModel.TownName), Times.Once);
        }

        [TestMethod]
        public void Invoke_AddressServiceCreate_When_Address_DoesNotExists_InTownFound()
        {
            // Arrange
            var username = "testUsername";
            var email = "test@email";
            var townName = "testTownName";
            var address = "testAdress";
            var userRegisterModel = new UserRegisterModel() { Username = username, EMail = email, TownName = townName, AddressText = address };

            var addressToAdd = new Address() { AddressText = address };
            var town = new Town() { Name = townName };
            var townsMock = new List<Town>() { town }.GetQueryableMockDbSet();
            var usersMock = new List<User>() { }.GetQueryableMockDbSet();

            var userToRegister = new User();

            var ctxMock = new Mock<IOnlineStoreContext>();
            var mapperMock = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceMock = new Mock<IAddressService>();

            var userService = new UserService(ctxMock.Object, mapperMock.Object, townServiceStub.Object, addressServiceMock.Object);

            Action addingAddressToTown =
                () => town.Addresses.Add(addressToAdd);

            ctxMock
                .Setup(ctx => ctx.Users)
                .Returns(usersMock.Object);

            ctxMock
                .Setup(ctx => ctx.Towns)
                .Returns(townsMock.Object);

            addressServiceMock
                .Setup(addServ => addServ.Create(userRegisterModel.AddressText, userRegisterModel.TownName))
                .Callback(addingAddressToTown);

            mapperMock
                .Setup(m => m.Map<User>(userRegisterModel))
                .Returns(userToRegister);

            // Act
            userService.RegisterUser(userRegisterModel);

            // Assert
            addressServiceMock.Verify(addServ => addServ.Create(userRegisterModel.AddressText, userRegisterModel.TownName), Times.Once);
        }

        [TestMethod]
        public void AddUser_ToDatabase_Validations_Pass()
        {
            // Arrange
            var username = "testUsername";
            var email = "test@email";
            var townName = "testTownName";
            var address = "testAdress";
            var userRegisterModel = new UserRegisterModel() { Username = username, EMail = email, TownName = townName, AddressText = address };

            var addressToAdd = new Address() { AddressText = address };
            var town = new Town() { Name = townName, Addresses = new List<Address>() { addressToAdd } };
            var townsMock = new List<Town>() { town }.GetQueryableMockDbSet();
            var usersMock = new List<User>() { }.GetQueryableMockDbSet();

            var userToRegister = new User();

            var ctxMock = new Mock<IOnlineStoreContext>();
            var mapperMock = new Mock<IMapper>();
            var townServiceMock = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxMock.Object, mapperMock.Object, townServiceMock.Object, addressServiceStub.Object);

            ctxMock
                .Setup(ctx => ctx.Users)
                .Returns(usersMock.Object);

            ctxMock
                .Setup(ctx => ctx.Towns)
                .Returns(townsMock.Object);

            mapperMock
                .Setup(m => m.Map<User>(userRegisterModel))
                .Returns(userToRegister);

            usersMock
                .Setup(u => u.Add(userToRegister))
                .Verifiable();

            // Act
            userService.RegisterUser(userRegisterModel);

            // Assert
            usersMock.Verify(u => u.Add(userToRegister), Times.Once);
        }
    }
}
