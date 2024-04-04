using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace YourNamespace.Tests
{
    [TestClass]
    public class ServiceRefactorTests
    {
        private Mock<IDataRepository> _mockDataRepository;
        private ServiceRefactor _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockDataRepository = new Mock<IDataRepository>();
            _service = new ServiceRefactor(_mockDataRepository.Object);
        }

        [TestMethod]
        public void UpsertCustomer_ValidUSCustomer_ReturnsCustomer()
        {
            TestCustomer("US", "12345");
        }

        [TestMethod]
        public void UpsertCustomer_ValidItalyCustomer_ReturnsCustomer()
        {
            TestCustomer("ITALY", "12345");
        }

        [TestMethod]
        public void UpsertCustomer_ValidGermanyCustomer_ReturnsCustomer()
        {
            TestCustomer("GERMANY", "12345");
        }

        [TestMethod]
        public void UpsertCustomer_ValidFranceCustomer_ReturnsCustomer()
        {
            TestCustomer("FRANCE", "12345");
        }

        private void TestCustomer(string state, string zipCode)
        {
            // Arrange
            var customer = new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main St",
                City = "New York",
                State = state,
                Phone = "1234567890",
                ZipCode = zipCode,
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Now.AddYears(-30)
            };

            // Act
            var result = _service.UpsertCustomer(customer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(customer.Id, result.Id);
            Assert.AreEqual(customer.FirstName, result.FirstName);
            Assert.AreEqual(customer.LastName, result.LastName);
            Assert.AreEqual(customer.Address, result.Address);
            Assert.AreEqual(customer.City, result.City);
            Assert.AreEqual(customer.State, result.State);
            Assert.AreEqual(customer.Phone, result.Phone);
            Assert.AreEqual(customer.ZipCode, result.ZipCode);
            Assert.AreEqual(customer.Email, result.Email);
            Assert.AreEqual(customer.DateOfBirth, result.DateOfBirth);

            _mockDataRepository.Verify(x => x.UpsertCustomer(customer), Times.Once);
        }
    }
}