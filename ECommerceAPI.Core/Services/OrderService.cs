using AutoMapper;
using ECommerceAPI.Core.DTOs.Order;
using ECommerceAPI.Core.Interfaces;
using ECommerceAPI.Domain.Entites;
using ECommerceAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Services
{
    public class OrderService : IOrderWriteService,IOrderReadService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IProductRepository productRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task CreateOrderAsync(CreateOrderDTO dto, int userId)
        {
            var cartItems = await _cartRepository.GetCartsByUserId(userId);
            if (!cartItems.Any())
            {
                throw new Exception("Sepet Boş!");
            }
            var totalPrice = cartItems.Sum(c => c.Product.Price * c.Quantity);

            var order = new Order
            {
                UserID = userId,
                AddressID = dto.AddressID,
                TotalPrice = totalPrice,
                OrderDate = DateTime.UtcNow,
                OrderStatus = "Pending"
            };

            await _orderRepository.AddAsync(order);

            foreach(var item in cartItems){
                var orderItem = new OrderItem
                {
                    OrderID = order.OrderID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                };
                await _orderRepository.AddOrderItemAsync(orderItem);
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDTO?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            return _mapper.Map<OrderDTO?>(order);   
        }

        public async Task<int> GetOrderCountAsync(int userId)
        {
            return await _orderRepository.GetOrderCountAsync(userId);
        }

        public async Task<OrderItemDTO?> GetOrderItemByIdAsync(int ProductId)
        {
            var order = await _orderRepository.GetOrderItemByIdAsync(ProductId);
            return _mapper.Map<OrderItemDTO?>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByStatusAsync(string status)
        {
            var orders = await _orderRepository.GetOrdersByStatusAsync(status);
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task UpdateOrderStatusAsync(UpdateOrderDTO dto)
        {
            var order = await _orderRepository.GetByIdAsync(dto.OrderID);
            if (order != null)
            {
                order.OrderStatus = dto.OrderStatus;
                await _orderRepository.UpdateAsync(order);
            }
        }
    }
}
