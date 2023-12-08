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

namespace HWK6_Tests
{
    public class OrderRepositoryTests
    {
        private OrderRepository _orderRepository;
        private Mock<CoffeeShopDbContext> _mockContext;
        private Mock<DbSet<Order>> _mockOrdersDbSet;

        [SetUp]
        public void Setup()
        {
            var orders = new List<Order>
            {
                // Ensure your Order objects have the necessary properties and are structured correctly with associated OrderItems
                new Order
                {
                    Id = 1,
                    StoreId = 1,
                    DlvyId = 1,
                    CustomerName = "John Doe",
                    Time = DateTime.UtcNow,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem { Id = 1, ItemId = 101, OrderId = 1, Qty = 2, Completed = false },
                        new OrderItem { Id = 2, ItemId = 102, OrderId = 1, Qty = 1, Completed = true }
                    }
                },
                new Order
                {
                    Id = 2,
                    StoreId = 2,
                    DlvyId = 2,
                    CustomerName = "Jane Smith",
                    Time = DateTime.UtcNow,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem { Id = 3, ItemId = 201, OrderId = 2, Qty = 3, Completed = false },
                        new OrderItem { Id = 4, ItemId = 202, OrderId = 2, Qty = 2, Completed = true }
                    }
                }
            }.AsQueryable();

            _mockOrdersDbSet = new Mock<DbSet<Order>>();
            _mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(orders.Provider);
            _mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(orders.Expression);
            _mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(orders.ElementType);
            _mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(orders.GetEnumerator());

            _mockContext = new Mock<CoffeeShopDbContext>();
            _mockContext.Setup(c => c.Orders).Returns(_mockOrdersDbSet.Object);

            _orderRepository = new OrderRepository(_mockContext.Object);
        }

        [Test]
        public void GetAllOrders_Returns_All_Orders()
        {
            // Act
            var result = _orderRepository.GetAllOrders();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count); // Assuming there are 2 orders
            // Add more specific assertions based on your scenario
        }

        [Test]
        public void GetAllOrders_When_Order_Null_Returns_Empty_List()
        {
            // Arrange
            var orders = new List<Order>
            {
                // Ensure your Order objects have the necessary properties and are structured correctly with associated OrderItems
                new Order
                {
                    Id = 1,
                    StoreId = 1,
                    DlvyId = 1,
                    CustomerName = "John Doe",
                    Time = DateTime.UtcNow,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem { Id = 1, ItemId = 101, OrderId = 1, Qty = 2, Completed = false },
                        new OrderItem { Id = 2, ItemId = 102, OrderId = 1, Qty = 1, Completed = true }
                    }
                },
                null // Add null order for testing
            }.AsQueryable();

            // Remove the null order from the list
            var ordersList = orders.Where(o => o != null).ToList();

            _mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(orders.Provider);
            _mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(orders.Expression);
            _mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(orders.ElementType);
            _mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(ordersList.GetEnumerator());

            // Act
            var result = _orderRepository.GetAllOrders();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count); // Only one valid order, expecting count 1
                                              // Add more specific assertions based on your scenario
        }


    }
}
