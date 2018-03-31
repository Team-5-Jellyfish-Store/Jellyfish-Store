using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Common.AutoMapperConfig;
using OnlineStore.Data.Contracts;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using OnlineStore.DTO.OrderModels.Constracts;
using OnlineStore.Models.DataModels;
using OnlineStore.Tests.Mocks;

namespace OnlineStore.Tests.Services.OrderService
{
    [TestClass]
    public class GetAllOrders_Should
    {
        [TestMethod]
        public void ReturnInstanceOfTypeIEnumerableIOrderProduct_WhenInvoked()
        {
            // Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var mockContext = new Mock<IOnlineStoreContext>();
            var usersMock = new List<User> { new User { Username = "Pesho" } }.GetQueryableMockDbSet();
            var couriersMock = new List<Courier> { new Courier { FirstName = "Peshko", LastName = "Peshkov" } }.GetQueryableMockDbSet();
            var ordersMock = new List<Order> { new Order() }.GetQueryableMockDbSet();
            var productsMock = new List<Product> { new Product { Name = "Testproduct", Quantity = 5 } }.GetQueryableMockDbSet();
            var orderProductsMock = new List<OrderProduct> { new OrderProduct() }.GetQueryableMockDbSet();

            IDictionary<string, int> productsCounts = new Dictionary<string, int>();
            productsCounts.Add("Testproduct", 2);
            mockContext.Setup(x => x.Users).Returns(usersMock.Object);
            mockContext.Setup(x => x.Couriers).Returns(couriersMock.Object);
            mockContext.Setup(s => s.Orders).Returns(ordersMock.Object);
            mockContext.Setup(s => s.Products).Returns(productsMock.Object);
            mockContext.Setup(s => s.OrderProducts).Returns(orderProductsMock.Object);

            var service = new Logic.Services.OrderService(mockContext.Object);

            // Act
            var orders = service.GetAllOrders();

            //Assert
            Assert.IsInstanceOfType(orders, typeof(IEnumerable<IOrderModel>));
        }

        [TestMethod]
        public void ReturnCorrectData_WhenInvoked()
        {
            // Arrange
            Mapper.Reset();
            AutomapperConfiguration.Initialize();
            var mockContext = new Mock<IOnlineStoreContext>();
            var stubDateTimeProvider = new MockDateTimeProvider();
            var orders = new List<Order> { new Order { Comment = "Patka", CourierId = 1, OrderedOn = stubDateTimeProvider.Now, UserId = 1, User = new User() { Username = "Pesho" } } };


            var mockSet = new Mock<DbSet<Order>>();

            mockSet.SetupData(orders);


            mockContext.Setup(s => s.Orders).Returns(mockSet.Object);

            var service = new Logic.Services.OrderService(mockContext.Object);


            // Act
            var ordersGot = service.GetAllOrders();

            //Assert
            Assert.AreEqual(orders.Count, ordersGot.Count());
        }
    }
}
