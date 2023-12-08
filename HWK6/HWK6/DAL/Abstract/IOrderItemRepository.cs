using System;
using Microsoft.AspNetCore.Mvc;
using HWK6.DAL.Abstract;
using HWK6.Models;
using HWK6.Models.DTO;


namespace HWK6.DAL.Abstract
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        List<OrderItemDTO> OrderItemByStation(string stationName);

    }
}