using HWK6.DAL.Abstract;
using HWK6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using HWK6.Models.DTO;
using HWK6.ExtensionMethods;

namespace HWK6.DAL.Concrete
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {

        private DbSet<Order> _orders;

        public OrderRepository(CoffeeShopDbContext context) : base(context)
        {
            _orders = context.Orders;

        }
        public List<OrderDTO> GetAllOrders()
        {
            var orders = _orders
                .Include(o => o.OrderItems) // Eager load OrderItems
                .ToList();

            var orderDTOs = orders
                .Where(o => o.OrderItems != null && o.OrderItems.Any()) // Check for null or empty OrderItems
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    StoreId = o.StoreId,
                    DlvyId = o.DlvyId,
                    CustomerName = o.CustomerName,
                    Time = o.Time,
                    Qty = o.OrderItems.Select(oi => oi.Qty).ToList(),
                    Completed = o.OrderItems.Select(oi => oi.Completed).ToList(),
                    Name = o.OrderItems.Select(oi => oi.Item?.Name).ToList(), // Check for null Item
                    Description = o.OrderItems.Select(oi => oi.Item?.Description).ToList(), // Check for null Item
                    Price = o.OrderItems.Select(oi => oi.Item?.Price ?? 0).ToList(), // Check for null Item
                    StationName = o.OrderItems.Select(oi => oi.Item?.Station?.StationName).ToList(), // Check for null Station or Item
                    StoreName = o.Store?.StoreName, // Check for null Store
                    DlvyName = o.Dlvy?.DlvyName, // Check for null Dlvy
                    TotalPrice = o.OrderItems.Sum(oi => oi.Qty * (oi.Item?.Price ?? 0)) // Check for null Item
                })
                .Where(o => o.Completed != null && o.Completed.Contains(false)) // Additional check for null Completed list
                .OrderBy(o => o.Time)
                .ToList();

            return orderDTOs;
        }
    }
}
