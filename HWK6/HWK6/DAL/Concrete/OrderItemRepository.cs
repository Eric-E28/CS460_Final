using HWK6.DAL.Abstract;
using HWK6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using HWK6.Models.DTO;
using HWK6.ExtensionMethods;
using System.Linq;

namespace HWK6.DAL.Concrete
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {

        private DbSet<OrderItem> _orderItems;

        public OrderItemRepository(CoffeeShopDbContext context) : base(context)
        {
            _orderItems = context.OrderItems;

        }

        public List<OrderItemDTO> OrderItemByStation(string stationName)
        {
            if (stationName == null)
            {
                // Handle the case when _orderItems is null, throw an exception or return an empty list
                throw new InvalidOperationException("_orderItems is null");
                // OR
                // return new List<OrderItemDTO>(); // Return an empty list
            }

            var orderItems = _orderItems
                .Where(o => o.Item != null && o.Item.Station != null && o.Item.Station.StationName == stationName && o.Completed == false)
                .Select(o => new OrderItemDTO
                {
                    Id = o.Id,
                    OrderId = o.OrderId,
                    ItemId = o.ItemId,
                    Qty = o.Qty,
                    Completed = o.Completed,
                    CustomerName = o.Order.CustomerName,
                    Time = o.Order.Time,
                    Name = o.Item.Name,
                    Description = o.Item.Description,
                    Price = o.Item.Price,
                    StationName = o.Item.Station.StationName,
                    StoreName = o.Order.Store.StoreName,
                    DlvyName = o.Order.Dlvy.DlvyName
                })
                .OrderBy(o => o.Time)
                .ToList();

            return orderItems;
        }

    }
}
