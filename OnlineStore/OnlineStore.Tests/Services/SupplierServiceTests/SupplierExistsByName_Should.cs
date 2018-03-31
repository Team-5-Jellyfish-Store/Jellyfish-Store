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

namespace OnlineStore.Tests.Services.SupplierServiceTests
{
    [TestClass]
    public class SupplierExistsByName_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_SupplierName_IsNullOrEmpty()
        {
            // Arrange
            var ctxStub = new Mock<IOnlineStoreContext>();
            var addressServiceStub = new Mock<IAddressService>();
            var townServiceStub = new Mock<ITownService>();
            var mapperStub = new Mock<IMapper>();

            var supplierService = new SupplierService(ctxStub.Object, addressServiceStub.Object, townServiceStub.Object, mapperStub.Object);

            Action executingSupplierExistsByNameMethod = () => supplierService.SupplierExistsByName(null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(executingSupplierExistsByNameMethod);
        }

        [TestMethod]
        public void ReturnTrue_When_Supplier_IsFound_InDatabase()
        {
            // Arrange
            var fakeSupplierName = "testName";

            var fakeSupplier = new Supplier() { Name = fakeSupplierName };
            var fakeSuppliers = new List<Supplier>() { fakeSupplier }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var addressServiceStub = new Mock<IAddressService>();
            var townServiceStub = new Mock<ITownService>();
            var mapperStub = new Mock<IMapper>();

            var supplierService = new SupplierService(ctxStub.Object, addressServiceStub.Object, townServiceStub.Object, mapperStub.Object);

            ctxStub
                .Setup(ctx => ctx.Suppliers)
                .Returns(fakeSuppliers.Object);

            // Act
            var result = supplierService.SupplierExistsByName(fakeSupplierName);

            // Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ReturnFalse_When_Supplier_IsNotFound_InDatabase()
        {
            // Arrange
            var fakeSupplierName = "testName";

            var fakeSuppliers = new List<Supplier>() { }.GetQueryableMockDbSet();

            var ctxStub = new Mock<IOnlineStoreContext>();
            var addressServiceStub = new Mock<IAddressService>();
            var townServiceStub = new Mock<ITownService>();
            var mapperStub = new Mock<IMapper>();

            var supplierService = new SupplierService(ctxStub.Object, addressServiceStub.Object, townServiceStub.Object, mapperStub.Object);

            ctxStub
                .Setup(ctx => ctx.Suppliers)
                .Returns(fakeSuppliers.Object);

            // Act
            var result = supplierService.SupplierExistsByName(fakeSupplierName);

            // Assert
            Assert.AreEqual(false, result);
        }
    }
}
