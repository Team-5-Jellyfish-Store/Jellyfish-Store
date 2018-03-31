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
    public class GetSupplierByName_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_SupplierName_IsNull()
        {
            // Arrange
            var ctxStub = new Mock<IOnlineStoreContext>();
            var addressServiceStub = new Mock<IAddressService>();
            var townServiceStub = new Mock<ITownService>();
            var mapperStub = new Mock<IMapper>();

            var supplierService = new SupplierService(ctxStub.Object, addressServiceStub.Object, townServiceStub.Object, mapperStub.Object);

            Action executingGetSupplierByNameMethod = () => supplierService.GetSupplierByName(null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(executingGetSupplierByNameMethod);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Supplier_IsNotFound_InDatabase()
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

            Action executingGetSupplierByNameMethod = () => supplierService.GetSupplierByName(fakeSupplierName);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingGetSupplierByNameMethod);
        }
    }
}
