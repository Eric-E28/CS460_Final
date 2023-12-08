using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HWK6.Models.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }

        public int? OrderId { get; set; }

        public int? ItemId { get; set; }

        public int? Qty { get; set; }

        public bool? Completed { get; set; }

        public string CustomerName { get; set; }

        public DateTime? Time { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string StationName { get; set; }

        public string StoreName { get; set; }

        public string DlvyName { get; set; }


    }
}

namespace HWK6.ExtensionMethods
{
    /// <summary>
    /// This class contains extension methods for the Seller class.  Extension methods allow you to add methods
    /// to existing classes without modifying the original class.  This is useful when you want to add functionality
    /// such as this: to convert from the internal Seller class to the external Seller DTO class.
    /// 
    /// This becomes a ToSeller() method in the Seller class.
    /// 
    /// You'll have to be very careful using these when your model has a Foreign Key property.  If you leave it out
    /// and then update/save changes then you'll lose the relationship.  If you modify a FK value and it's invalid
    /// you'll either get the wrong relation or you'll violate a constraint if it doesn't exist.
    /// </summary>
    public static class OrderItemExtensions
    {
        public static HWK6.Models.DTO.OrderItemDTO ToOrderItemDTO(this HWK6.Models.OrderItem orderItem)
        {
            return new HWK6.Models.DTO.OrderItemDTO
            {
                Id = orderItem.Id,
                OrderId = orderItem.OrderId,
                ItemId = orderItem.ItemId,
                Qty = orderItem.Qty,
                Completed = orderItem.Completed,
                CustomerName = orderItem.Order.CustomerName,
                Time = orderItem.Order.Time,
                Name = orderItem.Item.Name,
                Description = orderItem.Item.Description,
                Price = orderItem.Item.Price,
                StationName = orderItem.Item.Station.StationName,
                StoreName = orderItem.Order.Store.StoreName,
                DlvyName = orderItem.Order.Dlvy.DlvyName
            };
        }

        public static HWK6.Models.OrderItem ToOrderItem(this HWK6.Models.DTO.OrderItemDTO orderItem)
        {
            return new HWK6.Models.OrderItem
            {
                Id = orderItem.Id,
                OrderId = orderItem.OrderId,
                ItemId = orderItem.ItemId,
                Qty = orderItem.Qty,
                Completed = orderItem.Completed
            };
        }
    }
}