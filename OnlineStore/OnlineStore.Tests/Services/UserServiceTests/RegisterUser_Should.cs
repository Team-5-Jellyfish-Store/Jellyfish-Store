using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO.UserModels;
using OnlineStore.DTO.UserModels.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Logic.Services;
using OnlineStore.Models.DataModels;
using OnlineStore.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;

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
            var fakeUserRegisterModel = new Mock<IUserRegisterModel>();

            var fakeUser = new User() { Username = fakeUsername };
            var fakeUsers = new List<User>() { fakeUser }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxStub.Object, mapperStub.Object, townServiceStub.Object, addressServiceStub.Object);

            fakeUserRegisterModel
                .SetupGet(urm => urm.Username)
                .Returns(fakeUsername);

            ctxStub
                .Setup(x => x.Users)
                .Returns(fakeUsers.Object);

            Action executingRegisterUserMethod = () => userService.RegisterUser(fakeUserRegisterModel.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingRegisterUserMethod);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Email_AlreadyExists_InDatabase()
        {
            // Arrange
            var fakeUsername = "testUsername";
            var fakeEmail = "test@email";
            var fakeUserRegisterModel = new Mock<IUserRegisterModel>();

            var fakeUser = new User() { EMail = fakeEmail };
            var fakeUsers = new List<User>() { fakeUser }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxStub.Object, mapperStub.Object, townServiceStub.Object, addressServiceStub.Object);

            fakeUserRegisterModel
                .SetupGet(urm => urm.Username)
                .Returns(fakeUsername);

            fakeUserRegisterModel
                .SetupGet(urm => urm.EMail)
                .Returns(fakeEmail);

            ctxStub
                .Setup(x => x.Users)
                .Returns(fakeUsers.Object);

            Action executingRegisterUserMethod = () => userService.RegisterUser(fakeUserRegisterModel.Object);

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
            var fakeUserRegisterModel = new Mock<IUserRegisterModel>();

            var fakeAddress = new Address() { AddressText = fakeAddressText };

            var fakeTown = new Town() { Name = fakeTownName };
            var fakeTowns = new List<Town>() { }.GetQueryableMockDbSet();
            var newFakeTowns = new List<Town> { fakeTown }.GetQueryableMockDbSet();

            var fakeUsers = new List<User>() { }.GetQueryableMockDbSet();
            var userToRegisterStub = new User();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var mockTownService = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxStub.Object, mapperStub.Object, mockTownService.Object, addressServiceStub.Object);

            Action addingTownToTowns =
                () =>
                    ctxStub
                        .Setup(x => x.Towns)
                        .Returns(newFakeTowns.Object);

            Action addingAddressToTown =
                () => fakeTown.Addresses.Add(fakeAddress);


            fakeUserRegisterModel
                .SetupGet(urm => urm.Username)
                .Returns(fakeUsername);

            fakeUserRegisterModel
                .SetupGet(urm => urm.EMail)
                .Returns(fakeEmail);

            fakeUserRegisterModel
                .SetupGet(urm => urm.TownName)
                .Returns(fakeTownName);

            fakeUserRegisterModel
                .SetupGet(urm => urm.AddressText)
                .Returns(fakeAddressText);

            ctxStub
                .Setup(x => x.Users)
                .Returns(fakeUsers.Object);

            ctxStub
                .Setup(x => x.Towns)
                .Returns(fakeTowns.Object);

            mockTownService
                .Setup(ts => ts.Create(fakeTownName))
                .Callback(addingTownToTowns);

            addressServiceStub
                .Setup(addServ => addServ.Create(fakeAddressText, fakeTownName))
                .Callback(addingAddressToTown);

            mapperStub
                .Setup(m => m.Map<User>(fakeUserRegisterModel.Object))
                .Returns(userToRegisterStub);

            // Act
            userService.RegisterUser(fakeUserRegisterModel.Object);

            // Assert
            mockTownService.Verify(ts => ts.Create(fakeTownName), Times.Once);
        }

        [TestMethod]
        public void Invoke_AddressServiceCreate_When_Address_DoesNotExists_InTownFound()
        {
            // Arrange
            var fakeUsername = "testUsername";
            var fakeEmail = "test@email";
            var fakeTownName = "testTownName";
            var fakeAddressText = "testAdress";
            var fakeUserRegisterModel = new Mock<IUserRegisterModel>();

            var fakeAddress = new Address() { AddressText = fakeAddressText };

            var fakeTown = new Town() { Name = fakeTownName };
            var fakeTowns = new List<Town>() { fakeTown }.GetQueryableMockDbSet();

            var userToRegisterStub = new User();
            var fakeUsers = new List<User>() { }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var mockAddressService = new Mock<IAddressService>();

            var userService = new UserService(ctxStub.Object, mapperStub.Object, townServiceStub.Object, mockAddressService.Object);

            Action addingAddressToTown =
                () => fakeTown.Addresses.Add(fakeAddress);

            fakeUserRegisterModel
                .SetupGet(urm => urm.Username)
                .Returns(fakeUsername);

            fakeUserRegisterModel
                .SetupGet(urm => urm.EMail)
                .Returns(fakeEmail);

            fakeUserRegisterModel
                .SetupGet(urm => urm.TownName)
                .Returns(fakeTownName);

            fakeUserRegisterModel
                .SetupGet(urm => urm.AddressText)
                .Returns(fakeAddressText);

            ctxStub
                .Setup(ctx => ctx.Users)
                .Returns(fakeUsers.Object);

            ctxStub
                .Setup(ctx => ctx.Towns)
                .Returns(fakeTowns.Object);

            mockAddressService
                .Setup(addServ => addServ.Create(fakeAddressText, fakeTownName))
                .Callback(addingAddressToTown);

            mapperStub
                .Setup(m => m.Map<User>(fakeUserRegisterModel.Object))
                .Returns(userToRegisterStub);

            // Act
            userService.RegisterUser(fakeUserRegisterModel.Object);

            // Assert
            mockAddressService.Verify(addServ => addServ.Create(fakeAddressText, fakeTownName), Times.Once);
        }

        [TestMethod]
        public void Invoke_AddMethod_ToAddUser_ToUsers_When_Validations_Pass()
        {
            // Arrange
            var fakeUsername = "testUsername";
            var fakeEmail = "test@email";
            var fakeTownName = "testTownName";
            var fakeAddressText = "testAdress";
            var fakeUserRegisterModel = new Mock<IUserRegisterModel>();

            var fakeAddress = new Address() { AddressText = fakeAddressText };
            var fakeTown = new Town() { Name = fakeTownName, Addresses = new List<Address>() { fakeAddress } };
            var fakeTowns = new List<Town>() { fakeTown }.GetQueryableMockDbSet();

            var userToRegisterStub = new User();
            var mockUsers = new List<User>() { }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxStub.Object, mapperStub.Object, townServiceStub.Object, addressServiceStub.Object);

            fakeUserRegisterModel
                .SetupGet(urm => urm.Username)
                .Returns(fakeUsername);

            fakeUserRegisterModel
                .SetupGet(urm => urm.EMail)
                .Returns(fakeEmail);

            fakeUserRegisterModel
                .SetupGet(urm => urm.TownName)
                .Returns(fakeTownName);

            fakeUserRegisterModel
                .SetupGet(urm => urm.AddressText)
                .Returns(fakeAddressText);

            ctxStub
                .Setup(ctx => ctx.Users)
                .Returns(mockUsers.Object);

            ctxStub
                .Setup(ctx => ctx.Towns)
                .Returns(fakeTowns.Object);

            mapperStub
                .Setup(m => m.Map<User>(fakeUserRegisterModel.Object))
                .Returns(userToRegisterStub);

            mockUsers
                .Setup(u => u.Add(userToRegisterStub))
                .Verifiable();

            // Act
            userService.RegisterUser(fakeUserRegisterModel.Object);

            // Assert
            mockUsers.Verify(u => u.Add(userToRegisterStub), Times.Once);
        }

        [TestMethod]
        public void Invoke_ContextSaveChanges_When_Validations_Pass()
        {
            // Arrange
            var fakeUsername = "testUsername";
            var fakeEmail = "test@email";
            var fakeTownName = "testTownName";
            var fakeAddressText = "testAdress";
            var fakeUserRegisterModel = new Mock<IUserRegisterModel>();

            var fakeAddress = new Address() { AddressText = fakeAddressText };
            var fakeTown = new Town() { Name = fakeTownName, Addresses = new List<Address>() { fakeAddress } };
            var fakeTowns = new List<Town>() { fakeTown }.GetQueryableMockDbSet();

            var userToRegisterStub = new User();
            var fakeUsers = new List<User>() { }.GetQueryableMockDbSet();

            var mockCtx = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(mockCtx.Object, mapperStub.Object, townServiceStub.Object, addressServiceStub.Object);

            fakeUserRegisterModel
                .SetupGet(urm => urm.Username)
                .Returns(fakeUsername);

            fakeUserRegisterModel
                .SetupGet(urm => urm.EMail)
                .Returns(fakeEmail);

            fakeUserRegisterModel
                .SetupGet(urm => urm.TownName)
                .Returns(fakeTownName);

            fakeUserRegisterModel
                .SetupGet(urm => urm.AddressText)
                .Returns(fakeAddressText);

            mockCtx
                .Setup(ctx => ctx.Users)
                .Returns(fakeUsers.Object);

            mockCtx
                .Setup(ctx => ctx.Towns)
                .Returns(fakeTowns.Object);

            mapperStub
                .Setup(m => m.Map<User>(fakeUserRegisterModel.Object))
                .Returns(userToRegisterStub);

            fakeUsers
                .Setup(u => u.Add(userToRegisterStub))
                .Verifiable();

            // Act
            userService.RegisterUser(fakeUserRegisterModel.Object);

            // Assert
            mockCtx.Verify(ctx => ctx.SaveChanges(), Times.Once);
        }
    }
}
