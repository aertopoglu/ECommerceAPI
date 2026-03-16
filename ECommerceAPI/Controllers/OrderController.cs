using ECommerceAPI.Core.DTOs.Order;
using ECommerceAPI.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound("Order not found");

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role != "Admin" && order.UserID != userId) return Forbid();

            return Ok(order);
        }

        [HttpGet("status/{status}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            var orders = await _orderService.GetOrdersByStatusAsync(status);
            return Ok(orders);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetOrderCount()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var count = await _orderService.GetOrderCountAsync(userId);
            return Ok(count);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDTO orderdto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _orderService.CreateOrderAsync(orderdto, userId);
            return Ok("Order created");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(UpdateOrderDTO dto)
        {
            await _orderService.UpdateOrderStatusAsync(dto);
            return Ok("Order status updated!");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return Ok("Order deleted!");
        }
    }
}

