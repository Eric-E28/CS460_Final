//using HWK6.DAL.Abstract;
//using HWK6.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using System;
//using System.Threading.Tasks;
//using HWK6.Models.DTO;
//using HWK6.ExtensionMethods;

//namespace HWK6.DAL.Concrete
//{
//    public class OrderRequestData
//    {
//        // Define properties for order request data
//        public int Store { get; set; }
//        public int Dlvy { get; set; }
//        public string Name { get; set; }
//        public List<ItemData> Items { get; set; }
//    }

//    public class ItemData
//    {
//        // Define properties for items
//        public int Id { get; set; }
//        public int Qty { get; set; }
//    }

//    public class OrderGeneratorService : Repository<Order>, IOrderGeneratorService
//    {
//        private readonly ILogger<OrderGeneratorService> _logger;

//        public OrderGeneratorService(CoffeeShopDbContext context, ILogger<OrderGeneratorService> logger) : base(context)
//        {
//            _logger = logger;
//        }

//        public Order ProcessOrderData(string jsonData)
//        {
//            try
//            {
//                var orderRequestData = JsonConvert.DeserializeObject<OrderRequestData>(jsonData);

//                if (orderRequestData == null || orderRequestData.Items == null || orderRequestData.Items.Count == 0)
//                {
//                    _logger.LogError("Invalid order data");
//                    return null;
//                }

//                var receivedOrder = new Order
//                {
//                    StoreId = orderRequestData.Store,
//                    DlvyId = orderRequestData.Dlvy,
//                    CustomerName = orderRequestData.Name,
//                    Time = DateTime.Now
//                };

//                var orderItems = orderRequestData.Items
//                    .Select(item => new OrderItem
//                    {
//                        ItemId = item.Id,
//                        Qty = item.Qty,
//                        Completed = false
//                    })
//                    .ToList();

//                receivedOrder.OrderItems = orderItems;

//                return receivedOrder;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"An error occurred while processing order data: {ex.Message}");
//                return null;
//            }
//        }

//        public bool CreateOrUpdateOrder(Order order)
//        {
//            try
//            {
//                if (order == null)
//                {
//                    _logger.LogError("Invalid order data");
//                    return false;
//                }

//                var existingOrder = FindById(order.Id);

//                if (existingOrder == null)
//                {
//                    AddOrUpdate(order);
//                }
//                else
//                {
//                    // Update the existing order properties
//                    existingOrder.StoreId = order.StoreId;
//                    existingOrder.DlvyId = order.DlvyId;
//                    existingOrder.CustomerName = order.CustomerName;
//                    existingOrder.Time = order.Time;

//                    // If you have an OrderItems property in Order, update that here as well
//                    // existingOrder.OrderItems = order.OrderItems;

//                    AddOrUpdate(existingOrder);
//                }

//                return true;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"An error occurred while creating or updating order: {ex.Message}");
//                return false;
//            }
//        }
//    }
//}



using HWK6.DAL.Abstract;
using HWK6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HWK6.DAL.Concrete
{
    public class OrderRequestData
    {
        public int Store { get; set; }
        public int Dlvy { get; set; }
        public string Name { get; set; }
        public List<ItemData> Items { get; set; }
    }

    public class ItemData
    {
        public int Id { get; set; }
        public int Qty { get; set; }
    }

    public class OrderGeneratorService : Repository<Order>, IOrderGeneratorService
    {
        private readonly ILogger<OrderGeneratorService> _logger;

        public OrderGeneratorService(CoffeeShopDbContext context, ILogger<OrderGeneratorService> logger) : base(context)
        {
            _logger = logger;
        }

        public bool ProcessAndCreateOrUpdateOrder(string jsonData)
        {
            try
            {
                var orderRequestData = JsonConvert.DeserializeObject<OrderRequestData>(jsonData);

                if (orderRequestData == null || orderRequestData.Items == null || orderRequestData.Items.Count == 0)
                {
                    _logger.LogError("Invalid order data");
                    return false;
                }

                var receivedOrder = new Order
                {
                    StoreId = orderRequestData.Store,
                    DlvyId = orderRequestData.Dlvy,
                    CustomerName = orderRequestData.Name,
                    Time = DateTime.Now
                };

                var orderItems = orderRequestData.Items
                    .Select(item => new OrderItem
                    {
                        ItemId = item.Id,
                        Qty = item.Qty,
                        Completed = false
                    })
                    .ToList();

                receivedOrder.OrderItems = orderItems;

                // Create or update the order
                var existingOrder = FindById(receivedOrder.Id);

                if (existingOrder == null)
                {
                    AddOrUpdate(receivedOrder);
                }
                else
                {
                    // Update the existing order properties
                    existingOrder.StoreId = receivedOrder.StoreId;
                    existingOrder.DlvyId = receivedOrder.DlvyId;
                    existingOrder.CustomerName = receivedOrder.CustomerName;
                    existingOrder.Time = receivedOrder.Time;
                    existingOrder.OrderItems = receivedOrder.OrderItems; // Update order items if needed

                    AddOrUpdate(existingOrder);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while processing or creating/updating order: {ex.Message}");
                return false;
            }
        }
    }
}
