﻿using AutoMapper;
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
            var ctxStub = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxStub.Object, mapperStub.Object, townServiceStub.Object, addressServiceStub.Object);

            Action executingRegisterUserMethod = () => userService.RegisterUser(null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(executingRegisterUserMethod);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Username_AlreadyExists_InDatabase()
        {
            // Arrange
            var fakeUsername = "testUsername";
            var fakeUserRegisterModel = new UserRegisterModel() { Username = fakeUsername };

            var fakeUser = new User() { Username = fakeUsername };
            var fakeUsers = new List<User>() { fakeUser }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxStub.Object, mapperStub.Object, townServiceStub.Object, addressServiceStub.Object);

            ctxStub
                .Setup(x => x.Users)
                .Returns(fakeUsers.Object);

            Action executingRegisterUserMethod = () => userService.RegisterUser(fakeUserRegisterModel);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingRegisterUserMethod);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Email_AlreadyExists_InDatabase()
        {
            // Arrange
            var fakeUsername = "testUsername";
            var fakeEmail = "test@email";
            var fakeUserRegisterModel = new UserRegisterModel()
            {
                Username = fakeUsername,
                EMail = fakeEmail
            };

            var fakeUser = new User() { EMail = fakeEmail };
            var fakeUsers = new List<User>() { fakeUser }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxStub.Object, mapperStub.Object, townServiceStub.Object, addressServiceStub.Object);

            ctxStub
                .Setup(x => x.Users)
                .Returns(fakeUsers.Object);

            Action executingRegisterUserMethod = () => userService.RegisterUser(fakeUserRegisterModel);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingRegisterUserMethod);
        }

        [TestMethod]
        public void Invoke_TownServiceCreate_When_TownName_DoesNotExists_InDatabase()
        {
            // Arrange
            var fakeUsername = "testUsername";
            var fakeEmail = "test@email";
            var fakeTownName = "testTownName";
            var fakeAddressText = "testAdress";
            var fakeUserRegisterModel = new UserRegisterModel()
            {
                Username = fakeUsername,
                EMail = fakeEmail,
                TownName = fakeTownName,
                AddressText = fakeAddressText
            };

            var fakeTown = new Town() { Name = fakeTownName };
            var fakeAddress = new Address() { AddressText = fakeAddressText };

            var fakeUsers = new List<User>() { }.GetQueryableMockDbSet();
            var fakeTowns = new List<Town>() { }.GetQueryableMockDbSet();

            var userToRegisterStub = new User();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var mockTownService = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxStub.Object, mapperStub.Object, mockTownService.Object, addressServiceStub.Object);

            var newFakeTowns = new List<Town> { fakeTown }.GetQueryableMockDbSet();
            Action addingTownToTowns =
                () =>
                    ctxStub
                        .Setup(x => x.Towns)
                        .Returns(newFakeTowns.Object);

            Action addingAddressToTown =
                () => fakeTown.Addresses.Add(fakeAddress);

            ctxStub
                .Setup(x => x.Users)
                .Returns(fakeUsers.Object);

            ctxStub
                .Setup(x => x.Towns)
                .Returns(fakeTowns.Object);

            mockTownService
                .Setup(ts => ts.Create(fakeUserRegisterModel.TownName))
                .Callback(addingTownToTowns);

            addressServiceStub
                .Setup(addServ => addServ.Create(fakeUserRegisterModel.AddressText, fakeUserRegisterModel.TownName))
                .Callback(addingAddressToTown);

            mapperStub
                .Setup(m => m.Map<User>(fakeUserRegisterModel))
                .Returns(userToRegisterStub);

            // Act
            userService.RegisterUser(fakeUserRegisterModel);

            // Assert
            mockTownService.Verify(ts => ts.Create(fakeUserRegisterModel.TownName), Times.Once);
        }

        [TestMethod]
        public void Invoke_AddressServiceCreate_When_Address_DoesNotExists_InTownFound()
        {
            // Arrange
            var fakeUsername = "testUsername";
            var fakeEmail = "test@email";
            var fakeTownName = "testTownName";
            var fakeAddressText = "testAdress";
            var fakeUserRegisterModel = new UserRegisterModel() { Username = fakeUsername, EMail = fakeEmail, TownName = fakeTownName, AddressText = fakeAddressText };

            var fakeAddress = new Address() { AddressText = fakeAddressText };
            var fakeTown = new Town() { Name = fakeTownName };
            var fakeTowns = new List<Town>() { fakeTown }.GetQueryableMockDbSet();
            var fakeUsers = new List<User>() { }.GetQueryableMockDbSet();

            var userToRegisterStub = new User();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var mockAddressService = new Mock<IAddressService>();

            var userService = new UserService(ctxStub.Object, mapperStub.Object, townServiceStub.Object, mockAddressService.Object);

            Action addingAddressToTown =
                () => fakeTown.Addresses.Add(fakeAddress);

            ctxStub
                .Setup(ctx => ctx.Users)
                .Returns(fakeUsers.Object);

            ctxStub
                .Setup(ctx => ctx.Towns)
                .Returns(fakeTowns.Object);

            mockAddressService
                .Setup(addServ => addServ.Create(fakeUserRegisterModel.AddressText, fakeUserRegisterModel.TownName))
                .Callback(addingAddressToTown);

            mapperStub
                .Setup(m => m.Map<User>(fakeUserRegisterModel))
                .Returns(userToRegisterStub);

            // Act
            userService.RegisterUser(fakeUserRegisterModel);

            // Assert
            mockAddressService.Verify(addServ => addServ.Create(fakeUserRegisterModel.AddressText, fakeUserRegisterModel.TownName), Times.Once);
        }

        [TestMethod]
        public void AddUser_ToDatabase_When_Validations_Pass()
        {
            // Arrange
            var fakeUsername = "testUsername";
            var fakeEmail = "test@email";
            var fakeTownName = "testTownName";
            var fakeAddressText = "testAdress";
            var fakeUserRegisterModel = new UserRegisterModel() { Username = fakeUsername, EMail = fakeEmail, TownName = fakeTownName, AddressText = fakeAddressText };

            var fakeAddress = new Address() { AddressText = fakeAddressText };
            var fakeTown = new Town() { Name = fakeTownName, Addresses = new List<Address>() { fakeAddress } };
            var fakeTowns = new List<Town>() { fakeTown }.GetQueryableMockDbSet();
            var mockUsers = new List<User>() { }.GetQueryableMockDbSet();

            var userToRegisterStub = new User();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxStub.Object, mapperStub.Object, townServiceStub.Object, addressServiceStub.Object);

            ctxStub
                .Setup(ctx => ctx.Users)
                .Returns(mockUsers.Object);

            ctxStub
                .Setup(ctx => ctx.Towns)
                .Returns(fakeTowns.Object);

            mapperStub
                .Setup(m => m.Map<User>(fakeUserRegisterModel))
                .Returns(userToRegisterStub);

            mockUsers
                .Setup(u => u.Add(userToRegisterStub))
                .Verifiable();

            // Act
            userService.RegisterUser(fakeUserRegisterModel);

            // Assert
            mockUsers.Verify(u => u.Add(userToRegisterStub), Times.Once);
        }
    }
}
