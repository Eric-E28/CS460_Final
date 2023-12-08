using System;
using System.Collections.Generic;
using System.Linq;
using HWK6.DAL.Abstract;
using HWK6.DAL.Concrete;
using HWK6.Models;
using HWK6.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

// ... (imports remain the same)

namespace HWK6_Tests
{
    public class OrderItemRepositoryTests
    {
        private OrderItemRepository _orderItemRepository;
        private Mock<CoffeeShopDbContext> _mockContext;
        private Mock<DbSet<OrderItem>> _mockOrderItemsDbSet;

        [SetUp]
        public void Setup()
        {
            var orderItems = new List<OrderItem>
            {
                // Create sample OrderItems for testing
                new OrderItem { Id = 1, OrderId = 1, ItemId = 1, Qty = 2, Completed = false },
                new OrderItem { Id = 2, OrderId = 2, ItemId = 2, Qty = 1, Completed = true }
            }.AsQueryable();

            _mockOrderItemsDbSet = new Mock<DbSet<OrderItem>>();
            _mockOrderItemsDbSet.As<IQueryable<OrderItem>>().Setup(m => m.Provider).Returns(orderItems.Provider);
            _mockOrderItemsDbSet.As<IQueryable<OrderItem>>().Setup(m => m.Expression).Returns(orderItems.Expression);
            _mockOrderItemsDbSet.As<IQueryable<OrderItem>>().Setup(m => m.ElementType).Returns(orderItems.ElementType);
            _mockOrderItemsDbSet.As<IQueryable<OrderItem>>().Setup(m => m.GetEnumerator()).Returns(orderItems.GetEnumerator());

            _mockContext = new Mock<CoffeeShopDbContext>();
            _mockContext.Setup(c => c.OrderItems).Returns(_mockOrderItemsDbSet.Object);

            _orderItemRepository = new OrderItemRepository(_mockContext.Object);
        }

        [Test]
        public void OrderItemByStation_With_Null_StationName_Throws_Exception()
        {
            // Arrange, Act & Assert
            Assert.Throws<InvalidOperationException>(() => _orderItemRepository.OrderItemByStation(null));
        }

        [Test]
        public void OrderItemByStation_Returns_OrderItems_By_Station()
        {
            // Arrange
            var stationName = "Drinks"; // Replace with the station name you want to test

            // Act
            var result = _orderItemRepository.OrderItemByStation(stationName);

            // Assert
            Assert.IsNotNull(result);
            // Add specific assertions based on your scenario
        }
    }
}
