    using HWK6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HWK6.DAL.Abstract;
using HWK6.DAL.Concrete;
using HWK6.Models.DTO;
using HWK6.ExtensionMethods;

namespace HWK6.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderGeneratorService _orderGeneratorService;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderItemController(IOrderGeneratorService orderGeneratorService, IOrderItemRepository orderItemRepository, IOrderRepository orderRepository)
        {
            _orderGeneratorService = orderGeneratorService;
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
        }

        [HttpPost("receive")] // Unique route for this method
        public IActionResult ReceiveOrders([FromBody] OrderRequestData orderRequestData)
        {
            if (orderRequestData == null)
            {
                return BadRequest("Invalid order data");
            }

            string jsonData = JsonConvert.SerializeObject(orderRequestData);

            bool isCreated = _orderGeneratorService.ProcessAndCreateOrUpdateOrder(jsonData);

            if (isCreated)
            {
                return Ok("Order received and processed successfully");
            }
            else
            {
                return BadRequest("Failed to process order");
            }
        }

        [HttpGet("items")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<HWK6.Models.DTO.OrderItemDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<HWK6.Models.DTO.OrderItemDTO>> GetAllOrders()
        {
            var shows = _orderItemRepository
                .GetAll(
                    o => o.Order,       // Include Order navigation property
                    o => o.Item,        // Include Item navigation property
                    o => o.Item.Station, // Include Station navigation property for Item
                    o => o.Order.Store, // Include Store navigation property for Order
                    o => o.Order.Dlvy   // Include Dlvy navigation property for Order
                )
                .Select(o => o.ToOrderItemDTO())
                .ToList();

            if (shows.Count == 0)
            {
                return NotFound();
            }

            return shows;
        }

        [HttpGet("iorders")]
        [ProducesResponseType(200, Type = typeof(List<OrderDTO>))]
        public IActionResult GetOrders()
        {
            var orders = _orderRepository.GetAllOrders();

            return Ok(orders); // Return the list of DTOs
        }

        [HttpGet("station/{stationName}")]
        [ProducesResponseType(200, Type = typeof(List<OrderDTO>))]
        public IActionResult GetOrdersByStation(string stationName)
        {
            var orderItems = _orderItemRepository.OrderItemByStation(stationName);
            return Ok(orderItems);
        }

        [HttpPut("completed/{id}")]
        public IActionResult MarkOrderItemComplete(int id)
        {
            try
            {
                var orderItem = _orderItemRepository.FindById(id);

                if (orderItem == null)
                {
                    return NotFound("Order item not found");
                }

                // Update the completed status to true
                orderItem.Completed = true;

                // Save changes to the database
                _orderItemRepository.AddOrUpdate(orderItem);

                return Ok($"Order item {id} marked as completed");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error marking order item as completed: {ex.Message}");
            }
        }

    }
}
