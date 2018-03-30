using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Logic.Services;
using OnlineStore.Models.DataModels;
using OnlineStore.Tests.Mocks;
using System;
using System.Collections.Generic;

namespace OnlineStore.Tests.Services.CourierServiceTests
{
    [TestClass]
    public class CourierExistsByName_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_FirstNameOrLastName_IsNullOrEmpty()
        {
            // Arrange
            string fakeFirstName = null;
            string fakeLastName = "testLastName";

            var ctxStub = new Mock<IOnlineStoreContext>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();
            var mapperStub = new Mock<IMapper>();

            var courierService = new CourierService(ctxStub.Object, townServiceStub.Object, addressServiceStub.Object, mapperStub.Object);

            Action executingCourierExistsByNameMethod = () => courierService.CourierExistsByName(fakeFirstName, fakeLastName);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(executingCourierExistsByNameMethod);
        }

        [TestMethod]
        public void ReturnTrue_When_Courier_IsFound_InDatabase()
        {
            // Arrange
            string fakeFirstName = "testFirstName";
            string fakeLastName = "testLastName";

            var fakeCourier = new Courier() { FirstName = fakeFirstName, LastName = fakeLastName };
            var fakeCouriers = new List<Courier>() { fakeCourier }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();
            var mapperStub = new Mock<IMapper>();

            var courierService = new CourierService(ctxStub.Object, townServiceStub.Object, addressServiceStub.Object, mapperStub.Object);

            ctxStub
                .Setup(ctx => ctx.Couriers)
                .Returns(fakeCouriers.Object);

            // Act
            var result = courierService.CourierExistsByName(fakeFirstName, fakeLastName);

            // Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ReturnFalse_When_Courier_IsNotFound_InDatabase()
        {
            // Arrange
            string fakeFirstName = "testFirstName";
            string fakeLastName = "testLastName";

            var fakeCouriers = new List<Courier>() { }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();
            var mapperStub = new Mock<IMapper>();

            var courierService = new CourierService(ctxStub.Object, townServiceStub.Object, addressServiceStub.Object, mapperStub.Object);

            ctxStub
                .Setup(ctx => ctx.Couriers)
                .Returns(fakeCouriers.Object);

            // Act
            var result = courierService.CourierExistsByName(fakeFirstName, fakeLastName);

            // Assert
            Assert.AreEqual(false, result);
        }
    }
}
