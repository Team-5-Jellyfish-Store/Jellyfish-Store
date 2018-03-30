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
            var ctxStub = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxStub.Object, mapperStub.Object, townServiceStub.Object, addressServiceStub.Object);

            Action executingGetRegisteredUserMethod = () => userService.GetRegisteredUser(null);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingGetRegisteredUserMethod);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Username_DoesNot_Exists_InDatabse()
        {
            // Arrange
            string fakeUsername = "testUsername";
            var fakeUsers = new List<User>() { }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var mapperStub = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxStub.Object, mapperStub.Object, townServiceStub.Object, addressServiceStub.Object);

            ctxStub
                .Setup(ctx => ctx.Users)
                .Returns(fakeUsers.Object);

            Action executingGetRegisteredUserMethod = () => userService.GetRegisteredUser(fakeUsername);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingGetRegisteredUserMethod);
        }

        [TestMethod]
        public void Invoke_MapperMap_WithUser_FromDatabase()
        {
            // Arrange
            string fakeUsername = "testUsername";
            var fakeUser = new User() { Username = fakeUsername };
            var fakeUsers = new List<User>() { fakeUser }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var mockMapper = new Mock<IMapper>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();

            var userService = new UserService(ctxStub.Object, mockMapper.Object, townServiceStub.Object, addressServiceStub.Object);

            ctxStub
                .Setup(ctx => ctx.Users)
                .Returns(fakeUsers.Object);

            mockMapper
                .Setup(m => m.Map<IUserLoginModel>(fakeUser))
                .Verifiable();

            // Act
            userService.GetRegisteredUser(fakeUsername);

            // Assert
            mockMapper.Verify(m => m.Map<IUserLoginModel>(fakeUser), Times.Once);
        }
    }
}
