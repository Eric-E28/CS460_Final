using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using HWK6.Models;

namespace HWK6.Models.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int? StoreId { get; set; }
        public int? DlvyId { get; set; }

        [StringLength(255)]
        public string CustomerName { get; set; }
        public DateTime? Time { get; set; }
        public List<int?> Qty { get; set; }


        public List<bool?> Completed { get; set; }
        public List<string> Name { get; set; }
        public List<string> Description { get; set; }
        public List<double> Price { get; set; }
        public List<string> StationName { get; set; }
        public string StoreName { get; set; }
        public string DlvyName { get; set; }

        private double? _totalPrice;

        public double? TotalPrice
        {
            get => _totalPrice;
            set => _totalPrice = value != null ? (float?)Math.Round(value.Value, 2) : null;
        }
        public List<string> CompletionStatus
        {
            get
            {
                if (Completed != null)
                {
                    return Completed.Select(c => c == true ? "Completed" : "In-Progress").ToList();
                }
                return new List<string>();
            }
            set
            {
                // Setter can be implemented if needed
                // For read-only purposes, the setter can be left empty
            }
        }
    }
}

namespace HWK6.ExtensionMethods
{
    public static class OrderExtensions
    {
        public static HWK6.Models.DTO.OrderDTO ToOrderDTO(this HWK6.Models.Order order)
        {
            double totalPrice = order.OrderItems.Sum(oi => oi.Item.Price);

            return new HWK6.Models.DTO.OrderDTO
            {
                Id = order.Id,
                StoreId = order.StoreId,
                DlvyId = order.DlvyId,
                CustomerName = order.CustomerName,
                Time = order.Time,
                Qty = order.OrderItems.Select(oi => oi.Qty).ToList(),
                Completed = order.OrderItems.Select(oi => (bool?)oi.Completed).ToList(),
                Name = order.OrderItems.Select(oi => oi.Item.Name).ToList(),
                Description = order.OrderItems.Select(oi => oi.Item.Description).ToList(),
                Price = order.OrderItems.Select(oi => oi.Item.Price).ToList(),
                StationName = order.OrderItems.Select(oi => oi.Item.Station.StationName).ToList(),
                StoreName = order.Store.StoreName,
                DlvyName = order.Dlvy.DlvyName,
                TotalPrice = totalPrice
            };
        }

        public static HWK6.Models.Order ToOrder(this HWK6.Models.DTO.OrderDTO order)
        {
            return new HWK6.Models.Order
            {
                Id = order.Id,
                StoreId = order.StoreId,
                DlvyId = order.DlvyId,
                CustomerName = order.CustomerName,
                Time = order.Time
            };
        }
    }
}
