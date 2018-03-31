using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO.CourierModels;
using OnlineStore.Logic.Contracts;
using OnlineStore.Logic.Services;
using OnlineStore.Models.DataModels;
using OnlineStore.Tests.Mocks;
using System;
using System.Collections.Generic;

namespace OnlineStore.Tests.Services.CourierServiceTests
{
    [TestClass]
    public class AddCourierRange_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_CourierModels_IsNull()
        {
            // Arrange
            var ctxStub = new Mock<IOnlineStoreContext>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();
            var mapperStub = new Mock<IMapper>();

            var courierService = new CourierService(ctxStub.Object, townServiceStub.Object, addressServiceStub.Object, mapperStub.Object);

            Action executingAddCourierRangeMethod = () => courierService.AddCourierRange(null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(executingAddCourierRangeMethod);
        }

        [TestMethod]
        public void Invoke_TownServiceCreate_When_CourierModelTown_DoesNotExists_InDatabase()
        {
            // Arrange
            var fakeAddressText = "testAddress";
            var fakeTownName = "testTown";
            var fakeCourierImportModel = new Mock<ICourierImportModel>();
            var fakeCourierImportModels = new List<ICourierImportModel>() { fakeCourierImportModel.Object };

            var fakeTown = new Town() { Name = fakeTownName };
            var fakeTowns = new List<Town>() { }.GetQueryableMockDbSet();
            var newFakeTowns = new List<Town>() { fakeTown }.GetQueryableMockDbSet();

            var fakeAddress = new Address() { AddressText = fakeAddressText, Town = fakeTown };
            var fakeAddresses = new List<Address>() { }.GetQueryableMockDbSet();
            var newfakeAddresses = new List<Address>() { fakeAddress }.GetQueryableMockDbSet();

            var fakeCourier = new Mock<Courier>();
            var fakeCouriers = new List<Courier>() { }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var mockTownService = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();
            var mapperStub = new Mock<IMapper>();

            var courierService = new CourierService(ctxStub.Object, mockTownService.Object, addressServiceStub.Object, mapperStub.Object);

            Action addingTownToTowns =
                () =>
                    ctxStub
                        .Setup(ctx => ctx.Towns)
                        .Returns(newFakeTowns.Object);

            Action addingAddressToAddresses =
                () =>
                    ctxStub
                        .Setup(ctx => ctx.Addresses)
                        .Returns(newfakeAddresses.Object);

            fakeCourierImportModel
                    .SetupGet(cip => cip.Address)
                    .Returns(fakeAddressText);

            fakeCourierImportModel
                .SetupGet(cip => cip.Town)
                .Returns(fakeTownName);

            mapperStub
                .Setup(m => m.Map<ICourierImportModel, Courier>(fakeCourierImportModel.Object))
                .Returns(fakeCourier.Object);

            ctxStub
                .Setup(ctx => ctx.Towns)
                .Returns(fakeTowns.Object);

            ctxStub
                .Setup(ctx => ctx.Addresses)
                .Returns(fakeAddresses.Object);

            ctxStub
                .Setup(ctx => ctx.Couriers)
                .Returns(fakeCouriers.Object);

            mockTownService
                .Setup(ts => ts.Create(fakeTownName))
                .Callback(addingTownToTowns);

            addressServiceStub
                .Setup(addServ => addServ.Create(fakeAddressText, fakeTownName))
                .Callback(addingAddressToAddresses);

            // Act
            courierService.AddCourierRange(fakeCourierImportModels);

            // Assert
            mockTownService.Verify(ts => ts.Create(fakeTownName), Times.Once);
        }

        [TestMethod]
        public void Invoke_AddressServiceCreate_When_CourierModelAddress_DoesNotExists_InFoundTown()
        {
            // Arrange
            var fakeAddressText = "testAddress";
            var fakeTownName = "testTown";
            var fakeCourierImportModel = new Mock<ICourierImportModel>();
            var fakeCourierImportModels = new List<ICourierImportModel>() { fakeCourierImportModel.Object };

            var fakeTown = new Town() { Name = fakeTownName };
            var fakeTowns = new List<Town>() { fakeTown }.GetQueryableMockDbSet();

            var fakeAddress = new Address() { AddressText = fakeAddressText, Town = fakeTown };
            var fakeAddresses = new List<Address>() { }.GetQueryableMockDbSet();
            var newfakeAddresses = new List<Address>() { fakeAddress }.GetQueryableMockDbSet();

            var fakeCourier = new Mock<Courier>();
            var fakeCouriers = new List<Courier>() { }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var townServiceStub = new Mock<ITownService>();
            var mockAddressService = new Mock<IAddressService>();
            var mapperStub = new Mock<IMapper>();

            var courierService = new CourierService(ctxStub.Object, townServiceStub.Object, mockAddressService.Object, mapperStub.Object);

            Action addingAddressToAddresses =
                () =>
                    ctxStub
                        .Setup(ctx => ctx.Addresses)
                        .Returns(newfakeAddresses.Object);

            fakeCourierImportModel
                    .SetupGet(cip => cip.Address)
                    .Returns(fakeAddressText);

            fakeCourierImportModel
                .SetupGet(cip => cip.Town)
                .Returns(fakeTownName);

            mapperStub
                .Setup(m => m.Map<ICourierImportModel, Courier>(fakeCourierImportModel.Object))
                .Returns(fakeCourier.Object);

            ctxStub
                .Setup(ctx => ctx.Towns)
                .Returns(fakeTowns.Object);

            ctxStub
                .Setup(ctx => ctx.Addresses)
                .Returns(fakeAddresses.Object);

            ctxStub
                .Setup(ctx => ctx.Couriers)
                .Returns(fakeCouriers.Object);

            mockAddressService
                .Setup(addServ => addServ.Create(fakeAddressText, fakeTownName))
                .Callback(addingAddressToAddresses);

            // Act
            courierService.AddCourierRange(fakeCourierImportModels);

            // Assert
            mockAddressService.Verify(ts => ts.Create(fakeAddressText, fakeTownName), Times.Once);
        }

        [TestMethod]
        public void Invoke_AddMethod_ToAddCourier_ToCouriers_Once_PerCourier()
        {
            // Arrange
            var fakeAddressText = "testAddress";
            var fakeTownName = "testTown";
            var fakeCourierImportModel = new Mock<ICourierImportModel>();
            var fakeCourierImportModels = new List<ICourierImportModel>() { fakeCourierImportModel.Object };

            var fakeTown = new Town() { Name = fakeTownName };
            var fakeTowns = new List<Town>() { fakeTown }.GetQueryableMockDbSet();

            var fakeAddress = new Address() { AddressText = fakeAddressText, Town = fakeTown };
            var fakeAddresses = new List<Address>() { fakeAddress }.GetQueryableMockDbSet();

            var fakeCourier = new Mock<Courier>();
            var mockCouriers = new List<Courier>() { }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();
            var mapperStub = new Mock<IMapper>();

            var courierService = new CourierService(ctxStub.Object, townServiceStub.Object, addressServiceStub.Object, mapperStub.Object);

            fakeCourierImportModel
                    .SetupGet(cip => cip.Address)
                    .Returns(fakeAddressText);

            fakeCourierImportModel
                .SetupGet(cip => cip.Town)
                .Returns(fakeTownName);

            mapperStub
                .Setup(m => m.Map<ICourierImportModel, Courier>(fakeCourierImportModel.Object))
                .Returns(fakeCourier.Object);

            ctxStub
                .Setup(ctx => ctx.Towns)
                .Returns(fakeTowns.Object);

            ctxStub
                .Setup(ctx => ctx.Addresses)
                .Returns(fakeAddresses.Object);

            ctxStub
                .Setup(ctx => ctx.Couriers)
                .Returns(mockCouriers.Object);

            // Act
            courierService.AddCourierRange(fakeCourierImportModels);

            // Assert
            mockCouriers.Verify(c => c.Add(It.IsAny<Courier>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_ContextSaveChanges_When_ValidationsPass()
        {
            // Arrange
            var fakeAddressText = "testAddress";
            var fakeTownName = "testTown";
            var fakeCourierImportModel = new Mock<ICourierImportModel>();
            var fakeCourierImportModels = new List<ICourierImportModel>() { fakeCourierImportModel.Object };

            var fakeTown = new Town() { Name = fakeTownName };
            var fakeTowns = new List<Town>() { fakeTown }.GetQueryableMockDbSet();

            var fakeAddress = new Address() { AddressText = fakeAddressText, Town = fakeTown };
            var fakeAddresses = new List<Address>() { fakeAddress }.GetQueryableMockDbSet();

            var fakeCourier = new Mock<Courier>();
            var fakeCouriers = new List<Courier>() { }.GetQueryableMockDbSet();

            var mockCtx = new Mock<IOnlineStoreContext>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();
            var mapperStub = new Mock<IMapper>();

            var courierService = new CourierService(mockCtx.Object, townServiceStub.Object, addressServiceStub.Object, mapperStub.Object);

            fakeCourierImportModel
                    .SetupGet(cip => cip.Address)
                    .Returns(fakeAddressText);

            fakeCourierImportModel
                .SetupGet(cip => cip.Town)
                .Returns(fakeTownName);

            mapperStub
                .Setup(m => m.Map<ICourierImportModel, Courier>(fakeCourierImportModel.Object))
                .Returns(fakeCourier.Object);

            mockCtx
                .Setup(ctx => ctx.Towns)
                .Returns(fakeTowns.Object);

            mockCtx
                .Setup(ctx => ctx.Addresses)
                .Returns(fakeAddresses.Object);

            mockCtx
                .Setup(ctx => ctx.Couriers)
                .Returns(fakeCouriers.Object);

            // Act
            courierService.AddCourierRange(fakeCourierImportModels);

            // Assert
            mockCtx.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
