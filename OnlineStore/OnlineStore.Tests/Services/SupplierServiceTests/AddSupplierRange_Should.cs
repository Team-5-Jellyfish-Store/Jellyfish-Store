using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO.SupplierModels;
using OnlineStore.Logic.Contracts;
using OnlineStore.Logic.Services;
using OnlineStore.Models.DataModels;
using OnlineStore.Tests.Mocks;
using System;
using System.Collections.Generic;

namespace OnlineStore.Tests.Services.SupplierServiceTests
{
    [TestClass]
    public class AddSupplierRange_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_SupplierModels_IsNull()
        {
            // Arrange
            var ctxStub = new Mock<IOnlineStoreContext>();
            var addressServiceStub = new Mock<IAddressService>();
            var townServiceStub = new Mock<ITownService>();
            var mapperStub = new Mock<IMapper>();

            var supplierService = new SupplierService(ctxStub.Object, addressServiceStub.Object, townServiceStub.Object, mapperStub.Object);

            Action executingAddSupplierRangeMethod = () => supplierService.AddSupplierRange(null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(executingAddSupplierRangeMethod);
        }

        [TestMethod]
        public void Invoke_TownServiceCreate_When_SupplierModelTown_DoesNotExists_InDatabase()
        {
            // Arrange
            var fakeAddressText = "testAddress";
            var fakeTownName = "testTown";
            var fakeSupplierImportModel = new SuppliersImportModel() { Address = fakeAddressText, Town = fakeTownName };
            var fakeSupplierImportModels = new List<SuppliersImportModel>() { fakeSupplierImportModel };

            var fakeTown = new Town() { Name = fakeTownName };
            var fakeTowns = new List<Town>() { }.GetQueryableMockDbSet();
            var newFakeTowns = new List<Town>() { fakeTown }.GetQueryableMockDbSet();

            var fakeAddress = new Address() { AddressText = fakeAddressText, Town = fakeTown };
            var fakeAddresses = new List<Address>() { }.GetQueryableMockDbSet();
            var newfakeAddresses = new List<Address>() { fakeAddress }.GetQueryableMockDbSet();

            var fakeSupplier = new Mock<Supplier>();
            var fakeSuppliers = new List<Supplier>() { }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var mockTownService = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();
            var mapperStub = new Mock<IMapper>();

            var supplierService = new SupplierService(ctxStub.Object, addressServiceStub.Object, mockTownService.Object, mapperStub.Object);

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

            mapperStub
                .Setup(m => m.Map<ISuppliersImportModel, Supplier>(fakeSupplierImportModel))
                .Returns(fakeSupplier.Object);

            ctxStub
                .Setup(ctx => ctx.Towns)
                .Returns(fakeTowns.Object);

            ctxStub
                .Setup(ctx => ctx.Addresses)
                .Returns(fakeAddresses.Object);

            ctxStub
                .Setup(ctx => ctx.Suppliers)
                .Returns(fakeSuppliers.Object);

            mockTownService
                .Setup(ts => ts.Create(fakeTownName))
                .Callback(addingTownToTowns);

            addressServiceStub
                .Setup(addServ => addServ.Create(fakeAddressText, fakeTownName))
                .Callback(addingAddressToAddresses);

            // Act
            supplierService.AddSupplierRange(fakeSupplierImportModels);

            // Assert
            mockTownService.Verify(ts => ts.Create(fakeTownName), Times.Once);
        }

        [TestMethod]
        public void Invoke_AddressServiceCreate_When_SupplierModelAddress_DoesNotExists_InFoundTown()
        {
            // Arrange
            var fakeAddressText = "testAddress";
            var fakeTownName = "testTown";
            var fakeSupplierImportModel = new SuppliersImportModel() { Address = fakeAddressText, Town = fakeTownName };
            var fakeSupplierImportModels = new List<SuppliersImportModel>() { fakeSupplierImportModel };

            var fakeTown = new Town() { Name = fakeTownName };
            var fakeTowns = new List<Town>() { fakeTown }.GetQueryableMockDbSet();

            var fakeAddress = new Address() { AddressText = fakeAddressText, Town = fakeTown };
            var fakeAddresses = new List<Address>() { }.GetQueryableMockDbSet();
            var newfakeAddresses = new List<Address>() { fakeAddress }.GetQueryableMockDbSet();

            var fakeSupplier = new Mock<Supplier>();
            var fakeSuppliers = new List<Supplier>() { }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var townServiceStub = new Mock<ITownService>();
            var mockAddressService = new Mock<IAddressService>();
            var mapperStub = new Mock<IMapper>();

            var supplierService = new SupplierService(ctxStub.Object, mockAddressService.Object, townServiceStub.Object, mapperStub.Object);

            Action addingAddressToAddresses =
                () =>
                    ctxStub
                        .Setup(ctx => ctx.Addresses)
                        .Returns(newfakeAddresses.Object);

            mapperStub
                .Setup(m => m.Map<ISuppliersImportModel, Supplier>(fakeSupplierImportModel))
                .Returns(fakeSupplier.Object);

            ctxStub
                .Setup(ctx => ctx.Towns)
                .Returns(fakeTowns.Object);

            ctxStub
                .Setup(ctx => ctx.Addresses)
                .Returns(fakeAddresses.Object);

            ctxStub
                .Setup(ctx => ctx.Suppliers)
                .Returns(fakeSuppliers.Object);

            mockAddressService
                .Setup(addServ => addServ.Create(fakeAddressText, fakeTownName))
                .Callback(addingAddressToAddresses);

            // Act
            supplierService.AddSupplierRange(fakeSupplierImportModels);

            // Assert
            mockAddressService.Verify(addServ => addServ.Create(fakeAddressText, fakeTownName), Times.Once);
        }

        [TestMethod]
        public void Invoke_AddMethod_ToAddSupplier_ToSuppliers_Once_PerSupplier()
        {
            // Arrange
            var fakeAddressText = "testAddress";
            var fakeTownName = "testTown";
            var fakeSupplierImportModel = new SuppliersImportModel() { Address = fakeAddressText, Town = fakeTownName };
            var fakeSupplierImportModels = new List<SuppliersImportModel>() { fakeSupplierImportModel };

            var fakeTown = new Town() { Name = fakeTownName };
            var fakeTowns = new List<Town>() { fakeTown }.GetQueryableMockDbSet();

            var fakeAddress = new Address() { AddressText = fakeAddressText, Town = fakeTown };
            var fakeAddresses = new List<Address>() { fakeAddress }.GetQueryableMockDbSet();

            var fakeSupplier = new Mock<Supplier>();
            var mockSuppliers = new List<Supplier>() { }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();
            var mapperStub = new Mock<IMapper>();

            var supplierService = new SupplierService(ctxStub.Object, addressServiceStub.Object, townServiceStub.Object, mapperStub.Object);

            mapperStub
                .Setup(m => m.Map<ISuppliersImportModel, Supplier>(fakeSupplierImportModel))
                .Returns(fakeSupplier.Object);

            ctxStub
                .Setup(ctx => ctx.Towns)
                .Returns(fakeTowns.Object);

            ctxStub
                .Setup(ctx => ctx.Addresses)
                .Returns(fakeAddresses.Object);

            ctxStub
                .Setup(ctx => ctx.Suppliers)
                .Returns(mockSuppliers.Object);

            // Act
            supplierService.AddSupplierRange(fakeSupplierImportModels);

            // Assert
            mockSuppliers.Verify(s => s.Add(It.IsAny<Supplier>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_ContextSaveChanges_When_ValidationsPass()
        {
            // Arrange
            var fakeAddressText = "testAddress";
            var fakeTownName = "testTown";
            var fakeSupplierImportModel = new SuppliersImportModel() { Address = fakeAddressText, Town = fakeTownName };
            var fakeSupplierImportModels = new List<SuppliersImportModel>() { fakeSupplierImportModel };

            var fakeTown = new Town() { Name = fakeTownName };
            var fakeTowns = new List<Town>() { fakeTown }.GetQueryableMockDbSet();

            var fakeAddress = new Address() { AddressText = fakeAddressText, Town = fakeTown };
            var fakeAddresses = new List<Address>() { fakeAddress }.GetQueryableMockDbSet();

            var fakeSupplier = new Mock<Supplier>();
            var fakeSuppliers = new List<Supplier>() { }.GetQueryableMockDbSet();

            var mockCtx = new Mock<IOnlineStoreContext>();
            var townServiceStub = new Mock<ITownService>();
            var addressServiceStub = new Mock<IAddressService>();
            var mapperStub = new Mock<IMapper>();

            var supplierService = new SupplierService(mockCtx.Object, addressServiceStub.Object, townServiceStub.Object, mapperStub.Object);

            mapperStub
                .Setup(m => m.Map<ISuppliersImportModel, Supplier>(fakeSupplierImportModel))
                .Returns(fakeSupplier.Object);

            mockCtx
                .Setup(ctx => ctx.Towns)
                .Returns(fakeTowns.Object);

            mockCtx
                .Setup(ctx => ctx.Addresses)
                .Returns(fakeAddresses.Object);

            mockCtx
                .Setup(ctx => ctx.Suppliers)
                .Returns(fakeSuppliers.Object);

            // Act
            supplierService.AddSupplierRange(fakeSupplierImportModels);

            // Assert
            mockCtx.Verify(ctx => ctx.SaveChanges(), Times.Once);
        }
    }
}
