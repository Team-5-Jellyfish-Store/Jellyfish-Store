using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO.OrderModels;
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
            var usersMock = new List<User>().GetQueryableMockDbSet();

            mockContext.Setup(x => x.Users).Returns(usersMock.Object);

            var service = new Logic.Services.OrderService(mockContext.Object);
            var orderToMake = new OrderMakeModel
            {
                Username = "test"
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => service.MakeOrder(orderToMake));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenCouriersNotFoundAndUsernameExists()
        {
            // Arrange
            var mockContext = new Mock<IOnlineStoreContext>();
            var usersMock = new List<User> { new User { Username = "Pesho" } }.GetQueryableMockDbSet();
            var couriersMock = new List<Courier>().GetQueryableMockDbSet();

            mockContext.Setup(x => x.Users).Returns(usersMock.Object);
            mockContext.Setup(x => x.Couriers).Returns(couriersMock.Object);

            var service = new Logic.Services.OrderService(mockContext.Object);
            var orderToMake = new OrderMakeModel
            {
                Username = "Pesho"
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => service.MakeOrder(orderToMake));
        }

        [TestMethod]
        public void InvokeAddMethodOnOrders_WhenCourierFoundAndUsernameExists()
        {
            // Arrange
            var mockContext = new Mock<IOnlineStoreContext>();
            var usersMock = new List<User> { new User { Username = "Pesho" } }.GetQueryableMockDbSet();
            var couriersMock = new List<Courier> { new Courier { FirstName = "Peshko", LastName = "Peshkov" } }.GetQueryableMockDbSet();
            var ordersMock = new List<Order> { new Order() }.GetQueryableMockDbSet();
            var productsMock = new List<Product> { new Product { Name = "Testproduct", Quantity = 5 } }.GetQueryableMockDbSet();
            var orderProductsMock = new List<OrderProduct> { new OrderProduct() }.GetQueryableMockDbSet();

            IDictionary<string, int> productsCounts = new Dictionary<string, int>();
            productsCounts.Add("Testproduct", 2);
            var mockDateTimeProvider = new MockDateTimeProvider();

            mockContext.Setup(x => x.Users).Returns(usersMock.Object);
            mockContext.Setup(x => x.Couriers).Returns(couriersMock.Object);
            mockContext.Setup(s => s.Orders).Returns(ordersMock.Object);
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            mockContext.Setup(s => s.OrderProducts).Returns(orderProductsMock.Object);

            var service = new Logic.Services.OrderService(mockContext.Object);
            var orderToMake = new OrderMakeModel
            {
                Username = "Pesho",
                OrderedOn = mockDateTimeProvider.Now,
                Comment = "Tralala",
                ProductNameAndCounts = productsCounts
            };

            // Act
            service.MakeOrder(orderToMake);

            //Assert
            ordersMock.Verify(v => v.Add(It.IsNotNull<Order>()), Times.Once);
        }

        [TestMethod]
        public void AddOrderToOrders_WhenCourierFoundAndUsernameExists()
        {
            // Arrange
            var mockContext = new Mock<IOnlineStoreContext>();
            var usersMock = new List<User> { new User { Username = "Pesho" } }.GetQueryableMockDbSet();
            var couriersMock = new List<Courier> { new Courier { FirstName = "Peshko", LastName = "Peshkov" } }.GetQueryableMockDbSet();
            var ordersMock = new List<Order> { new Order() }.GetQueryableMockDbSet();
            var productsMock = new List<Product> { new Product { Name = "Testproduct", Quantity = 5 } }.GetQueryableMockDbSet();
            var orderProductsMock = new List<OrderProduct> { new OrderProduct() }.GetQueryableMockDbSet();

            IDictionary<string, int> productsCounts = new Dictionary<string, int>();
            productsCounts.Add("Testproduct", 2);
            var mockDateTimeProvider = new MockDateTimeProvider();

            mockContext.Setup(x => x.Users).Returns(usersMock.Object);
            mockContext.Setup(x => x.Couriers).Returns(couriersMock.Object);
            mockContext.Setup(s => s.Orders).Returns(ordersMock.Object);
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            mockContext.Setup(s => s.OrderProducts).Returns(orderProductsMock.Object);

            var service = new Logic.Services.OrderService(mockContext.Object);
            var orderToMake = new OrderMakeModel
            {
                Username = "Pesho",
                OrderedOn = mockDateTimeProvider.Now,
                Comment = "Tralala",
                ProductNameAndCounts = productsCounts
            };

            // Act
            service.MakeOrder(orderToMake);

            //Assert
            Assert.AreEqual(1, mockContext.Object.Orders.Count());
        }

        [TestMethod]
        public void ThrowArgumentException_WhenProductDoesNotExist()
        {
            // Arrange
            var mockContext = new Mock<IOnlineStoreContext>();
            var usersMock = new List<User> { new User { Username = "Pesho" } }.GetQueryableMockDbSet();
            var couriersMock = new List<Courier> { new Courier { FirstName = "Peshko", LastName = "Peshkov" } }.GetQueryableMockDbSet();
            var ordersMock = new List<Order> { new Order() }.GetQueryableMockDbSet();
            var productsMock = new List<Product> { new Product { Name = "Testproduct", Quantity = 5 } }.GetQueryableMockDbSet();
            var orderProductsMock = new List<OrderProduct> { new OrderProduct() }.GetQueryableMockDbSet();

            IDictionary<string, int> productsCounts = new Dictionary<string, int>();
            productsCounts.Add("Testproducttt", 2);
            var mockDateTimeProvider = new MockDateTimeProvider();

            mockContext.Setup(x => x.Users).Returns(usersMock.Object);
            mockContext.Setup(x => x.Couriers).Returns(couriersMock.Object);
            mockContext.Setup(s => s.Orders).Returns(ordersMock.Object);
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            mockContext.Setup(s => s.OrderProducts).Returns(orderProductsMock.Object);

            var service = new Logic.Services.OrderService(mockContext.Object);
            var orderToMake = new OrderMakeModel
            {
                Username = "Pesho",
                OrderedOn = mockDateTimeProvider.Now,
                Comment = "Tralala",
                ProductNameAndCounts = productsCounts
            };

            //Act && Assert
            Assert.ThrowsException<ArgumentException>(() => service.MakeOrder(orderToMake));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenQuantityIsNotEnough()
        {
            // Arrange
            var mockContext = new Mock<IOnlineStoreContext>();
            var usersMock = new List<User> { new User { Username = "Pesho" } }.GetQueryableMockDbSet();
            var couriersMock = new List<Courier> { new Courier { FirstName = "Peshko", LastName = "Peshkov" } }.GetQueryableMockDbSet();
            var ordersMock = new List<Order> { new Order() }.GetQueryableMockDbSet();
            var productsMock = new List<Product> { new Product { Name = "Testproduct", Quantity = 5 } }.GetQueryableMockDbSet();
            var orderProductsMock = new List<OrderProduct> { new OrderProduct() }.GetQueryableMockDbSet();

            IDictionary<string, int> productsCounts = new Dictionary<string, int>();
            productsCounts.Add("Testproduct", 7);
            var mockDateTimeProvider = new MockDateTimeProvider();

            mockContext.Setup(x => x.Users).Returns(usersMock.Object);
            mockContext.Setup(x => x.Couriers).Returns(couriersMock.Object);
            mockContext.Setup(s => s.Orders).Returns(ordersMock.Object);
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            mockContext.Setup(s => s.OrderProducts).Returns(orderProductsMock.Object);

            var service = new Logic.Services.OrderService(mockContext.Object);
            var orderToMake = new OrderMakeModel
            {
                Username = "Pesho",
                OrderedOn = mockDateTimeProvider.Now,
                Comment = "Tralala",
                ProductNameAndCounts = productsCounts
            };

            //Act && Assert
            Assert.ThrowsException<ArgumentException>(() => service.MakeOrder(orderToMake));
        }

        [TestMethod]
        public void AddOrderProductToOrderProducts_WhenCourierFoundAndUsernameExistsAndQuantityIsAvailableAndProductIsFound()
        {
            // Arrange
            var mockContext = new Mock<IOnlineStoreContext>();
            var usersMock = new List<User> { new User { Username = "Pesho" } }.GetQueryableMockDbSet();
            var couriersMock = new List<Courier> { new Courier { FirstName = "Peshko", LastName = "Peshkov" } }.GetQueryableMockDbSet();
            var ordersMock = new List<Order> { new Order() }.GetQueryableMockDbSet();
            var productsMock = new List<Product> { new Product { Name = "Testproduct", Quantity = 5 } }.GetQueryableMockDbSet();
            var orderProductsMock = new List<OrderProduct> { new OrderProduct() }.GetQueryableMockDbSet();

            IDictionary<string, int> productsCounts = new Dictionary<string, int>();
            productsCounts.Add("Testproduct", 2);
            var mockDateTimeProvider = new MockDateTimeProvider();

            mockContext.Setup(x => x.Users).Returns(usersMock.Object);
            mockContext.Setup(x => x.Couriers).Returns(couriersMock.Object);
            mockContext.Setup(s => s.Orders).Returns(ordersMock.Object);
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            mockContext.Setup(s => s.OrderProducts).Returns(orderProductsMock.Object);

            var service = new Logic.Services.OrderService(mockContext.Object);
            var orderToMake = new OrderMakeModel
            {
                Username = "Pesho",
                OrderedOn = mockDateTimeProvider.Now,
                Comment = "Tralala",
                ProductNameAndCounts = productsCounts
            };

            // Act
            service.MakeOrder(orderToMake);

            //Assert
            Assert.AreEqual(1, mockContext.Object.Orders.Count());
        }

        [TestMethod]
        public void ReduceTheAvailableProductQuantity_WhenOrderIsFinished()
        {
            // Arrange
            var mockContext = new Mock<IOnlineStoreContext>();
            var usersMock = new List<User> { new User { Username = "Pesho" } }.GetQueryableMockDbSet();
            var couriersMock = new List<Courier> { new Courier { FirstName = "Peshko", LastName = "Peshkov" } }.GetQueryableMockDbSet();
            var ordersMock = new List<Order> { new Order() }.GetQueryableMockDbSet();
            var productsMock = new List<Product> { new Product { Name = "Testproduct", Quantity = 5 } }.GetQueryableMockDbSet();
            var orderProductsMock = new List<OrderProduct> { new OrderProduct() }.GetQueryableMockDbSet();

            IDictionary<string, int> productsCounts = new Dictionary<string, int>();
            productsCounts.Add("Testproduct", 2);
            var mockDateTimeProvider = new MockDateTimeProvider();

            mockContext.Setup(x => x.Users).Returns(usersMock.Object);
            mockContext.Setup(x => x.Couriers).Returns(couriersMock.Object);
            mockContext.Setup(s => s.Orders).Returns(ordersMock.Object);
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            mockContext.Setup(s => s.OrderProducts).Returns(orderProductsMock.Object);

            var service = new Logic.Services.OrderService(mockContext.Object);
            var orderToMake = new OrderMakeModel
            {
                Username = "Pesho",
                OrderedOn = mockDateTimeProvider.Now,
                Comment = "Tralala",
                ProductNameAndCounts = productsCounts
            };

            // Act
            service.MakeOrder(orderToMake);

            //Assert
            Assert.AreEqual(3, mockContext.Object.Products.FirstOrDefault(f => f.Name == "Testproduct").Quantity);
        }

        [TestMethod]
        public void InvokeAddMethodOnOrderProducts_WhenCourierFoundAndUsernameExists()
        {
            // Arrange
            var mockContext = new Mock<IOnlineStoreContext>();
            var usersMock = new List<User> { new User { Username = "Pesho" } }.GetQueryableMockDbSet();
            var couriersMock = new List<Courier> { new Courier { FirstName = "Peshko", LastName = "Peshkov" } }.GetQueryableMockDbSet();
            var ordersMock = new List<Order> { new Order() }.GetQueryableMockDbSet();
            var productsMock = new List<Product> { new Product { Name = "Testproduct", Quantity = 5 } }.GetQueryableMockDbSet();
            var orderProductsMock = new List<OrderProduct> { new OrderProduct() }.GetQueryableMockDbSet();

            IDictionary<string, int> productsCounts = new Dictionary<string, int>();
            productsCounts.Add("Testproduct", 2);
            var mockDateTimeProvider = new MockDateTimeProvider();

            mockContext.Setup(x => x.Users).Returns(usersMock.Object);
            mockContext.Setup(x => x.Couriers).Returns(couriersMock.Object);
            mockContext.Setup(s => s.Orders).Returns(ordersMock.Object);
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            mockContext.Setup(s => s.OrderProducts).Returns(orderProductsMock.Object);

            var service = new Logic.Services.OrderService(mockContext.Object);
            var orderToMake = new OrderMakeModel
            {
                Username = "Pesho",
                OrderedOn = mockDateTimeProvider.Now,
                Comment = "Tralala",
                ProductNameAndCounts = productsCounts
            };

            // Act
            service.MakeOrder(orderToMake);

            //Assert
            orderProductsMock.Verify(v => v.Add(It.IsNotNull<OrderProduct>()), Times.Once);
        }

        [TestMethod]
        public void InvokeSaveChanges_WhenMethodIsFinished()
        {
            // Arrange
            var mockContext = new Mock<IOnlineStoreContext>();
            var usersMock = new List<User> { new User { Username = "Pesho" } }.GetQueryableMockDbSet();
            var couriersMock = new List<Courier> { new Courier { FirstName = "Peshko", LastName = "Peshkov" } }.GetQueryableMockDbSet();
            var ordersMock = new List<Order> { new Order() }.GetQueryableMockDbSet();
            var productsMock = new List<Product> { new Product { Name = "Testproduct", Quantity = 5 } }.GetQueryableMockDbSet();
            var orderProductsMock = new List<OrderProduct> { new OrderProduct() }.GetQueryableMockDbSet();

            IDictionary<string, int> productsCounts = new Dictionary<string, int>();
            productsCounts.Add("Testproduct", 2);
            var mockDateTimeProvider = new MockDateTimeProvider();

            mockContext.Setup(x => x.Users).Returns(usersMock.Object);
            mockContext.Setup(x => x.Couriers).Returns(couriersMock.Object);
            mockContext.Setup(s => s.Orders).Returns(ordersMock.Object);
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            mockContext.Setup(s => s.OrderProducts).Returns(orderProductsMock.Object);

            var service = new Logic.Services.OrderService(mockContext.Object);
            var orderToMake = new OrderMakeModel
            {
                Username = "Pesho",
                OrderedOn = mockDateTimeProvider.Now,
                Comment = "Tralala",
                ProductNameAndCounts = productsCounts
            };

            // Act
            service.MakeOrder(orderToMake);

            //Assert
            mockContext.Verify(v => v.SaveChanges(), Times.Once);
        }

        //[TestMethod]
        //public void SetTheRightCommentFromTheModel_WhenMakingTheOrder()
        //{
        //    // Arrange
        //    var mockContext = new Mock<IOnlineStoreContext>();
        //    var usersMock = new List<User> { new User { Username = "Pesho" } }.GetQueryableMockDbSet();
        //    var couriersMock = new List<Courier> { new Courier { FirstName = "Peshko", LastName = "Peshkov" } }.GetQueryableMockDbSet();
        //    var ordersMock = new List<Order> { new Order() }.GetQueryableMockDbSet();
        //    var productsMock = new List<Product> { new Product { Name = "Testproduct", Quantity = 5 } }.GetQueryableMockDbSet();
        //    var orderProductsMock = new List<OrderProduct> { new OrderProduct() }.GetQueryableMockDbSet();

        //    IDictionary<string, int> productsCounts = new Dictionary<string, int>();
        //    productsCounts.Add("Testproduct", 2);
        //    var mockDateTimeProvider = new MockDateTimeProvider();

        //    mockContext.Setup(x => x.Users).Returns(usersMock.Object);
        //    mockContext.Setup(x => x.Couriers).Returns(couriersMock.Object);
        //    mockContext.Setup(s => s.Orders).Returns(ordersMock.Object);
        //    mockContext.Setup(s => s.Products).Returns(productsMock.Object);
        //    mockContext.Setup(s => s.OrderProducts).Returns(orderProductsMock.Object);

        //    var service = new Logic.Services.OrderService(mockContext.Object);
        //    var orderToMake = new OrderMakeModel
        //    {
        //        Username = "Pesho",
        //        OrderedOn = mockDateTimeProvider.Now,
        //        Comment = "Tralala",
        //        ProductNameAndCounts = productsCounts
        //    };
        //    var expectedComment = orderToMake.Comment;

        //    // Act
        //    service.MakeOrder(orderToMake);

        //    var actualComment = mockContext.Object.Orders.FirstOrDefault(f => f.User.Username == "Pesho").Comment;

        //    //Assert
        //    Assert.AreEqual(expectedComment, actualComment);
        //}
    }
}
