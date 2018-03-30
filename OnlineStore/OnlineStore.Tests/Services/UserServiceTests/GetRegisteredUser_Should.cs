using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO.UserModels.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Logic.Services;
using OnlineStore.Models.DataModels;
using OnlineStore.Tests.Mocks;
using System;
using System.Collections.Generic;

namespace OnlineStore.Tests.Services.UserServiceTests
{
    [TestClass]
    public class GetRegisteredUser_Should
    {
        [TestMethod]
        public void Throw_ArgumentException_When_UserName_IsNullOrEmpty()
        {
            // Arrange
            var ctxMock = new Mock<IOnlineStoreContext>();
            var mapperMock = new Mock<IMapper>();
            var townServiceMock = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxMock.Object, mapperMock.Object, townServiceMock.Object, addressServiceStub.Object);

            Action executingGetRegisteredUserMethod = () => userService.GetRegisteredUser(null);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingGetRegisteredUserMethod);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Username_DoesNot_Exists_InDatabse()
        {
            // Arrange
            string username = "testUsername";
            var fakeUsers = new List<User>() { }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxStub.Object, mapperStub.Object, townServiceStub.Object, addressServiceStub.Object);

            ctxStub
                .Setup(ctx => ctx.Users)
                .Returns(fakeUsers.Object);

            Action executingGetRegisteredUserMethod = () => userService.GetRegisteredUser(username);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingGetRegisteredUserMethod);
        }

        [TestMethod]
        public void Invoke_MapperMap_WithUser_FromDatabase()
        {
            // Arrange
            string username = "testUsername";
            var fakeUser = new User() { Username = username };
            var fakeUsers = new List<User>() { fakeUser }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxStub.Object, mapperStub.Object, townServiceStub.Object, addressServiceStub.Object);

            ctxStub
                .Setup(ctx => ctx.Users)
                .Returns(fakeUsers.Object);

            mapperStub
                .Setup(m => m.Map<IUserLoginModel>(fakeUser))
                .Verifiable();

            // Act
            userService.GetRegisteredUser(username);

            // Assert
            mapperStub.Verify(m => m.Map<IUserLoginModel>(fakeUser), Times.Once);
        }
    }
}
