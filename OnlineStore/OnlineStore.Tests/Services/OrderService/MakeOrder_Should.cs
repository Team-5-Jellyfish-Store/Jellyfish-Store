using System;
using System.Collections.Generic;
using System.Data.Entity;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO.OrderModels;
using OnlineStore.DTO.OrderModels.Constracts;
using OnlineStore.Models.DataModels;
using OnlineStore.Tests.Mocks;

namespace OnlineStore.Tests.Services.OrderService
{
    [TestClass]
    public class MakeOrder_Should
    {
        [TestMethod]
        public void ThrowArgumentException_WhenOrderModelIsNull()
        {
            //Arrange
            var fakeContext = new Mock<IOnlineStoreContext>();
            var orderModel = new Logic.Services.OrderService(fakeContext.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => orderModel.MakeOrder(null));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenUserNotFound()
        {
            // Arrange
            var mockContext = new Mock<IOnlineStoreContext>();
            List<User> users = new List<User>() { new User() { Username = "Pesho" } };

            var usersMock = users.GetQueryableMockDbSet<User>();

            mockContext.Setup(x => x.Users).Returns(usersMock.Object);

            var service = new Logic.Services.OrderService(mockContext.Object);
            var orderToMake = new OrderMakeModel()
            {
                Username = "pp"
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => service.MakeOrder(orderToMake));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenCouriersNotFoundAndUsernameExists()
        {
            // Arrange
            var mockContext = new Mock<IOnlineStoreContext>();
            List<User> users = new List<User>() { new User() { Username = "Pesho" } };
            List<Courier> couriers = new List<Courier>();

            var usersMock = users.GetQueryableMockDbSet<User>();
            var couriersMock = couriers.GetQueryableMockDbSet<Courier>();

            mockContext.Setup(x => x.Users).Returns(usersMock.Object);
            mockContext.Setup(x => x.Couriers).Returns(couriersMock.Object);

            var service = new Logic.Services.OrderService(mockContext.Object);
            var orderToMake = new OrderMakeModel()
            {
                Username = "Pesho"
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => service.MakeOrder(orderToMake));
        }

        //[TestMethod]
        //public void InvokesOrderMethod_WhenCourierFoundAndUsernameExists()
        //{
        //    // Arrange
        //    var mockContext = new Mock<IOnlineStoreContext>();
        //    List<User> users = new List<User>() { new User { Username = "Pesho" } };
        //    List<Courier> couriers = new List<Courier>{new Courier {FirstName = "Peshko", LastName = "Peshkov"}};
        //    List<Order> orders = new List<Order>{new Order()};

        //    var usersMock = users.GetQueryableMockDbSet<User>();
        //    var couriersMock = couriers.GetQueryableMockDbSet<Courier>();
        //    var ordersMock = orders.GetQueryableMockDbSet<Order>();

        //    mockContext.Setup(x => x.Users).Returns(usersMock.Object);
        //    mockContext.Setup(x => x.Couriers).Returns(couriersMock.Object);
        //    ordersMock.Setup(x => x.Add(It.IsNotNull<Order>())).Verifiable();


        //    var service = new Logic.Services.OrderService(mockContext.Object);
        //    var orderToMake = new OrderMakeModel()
        //    {
        //        Username = "Pesho"
        //    };

        //    // Act
        //    service.MakeOrder(orderToMake);

        //    //Assert

        //    ordersMock.Verify(v => v.Add(It.IsAny<Order>()), Times.Once);
            
        //}

        //[TestMethod]
        //public void InvokesUsersCollection_WhenOrderIsNotNull()
        //{
        //    // Arrange
        //    var mockContext = new Mock<IOnlineStoreContext>();
        //    List<User> users = new List<User>() { new User() { Username = "Pesho" } };

        //    var usersMock = users.GetQueryableMockDbSet<User>();
        //    mockContext.Setup(s => s.Users).Verifiable();
        //    var service = new Logic.Services.OrderService(mockContext.Object);
        //    var orderToMake = new OrderMakeModel()
        //    {
        //        Username = "pp"
        //    };

        //    //Act && Assert

        //    mockContext.Verify(v => v.Users, Times.Once);

        //}
    }
}
