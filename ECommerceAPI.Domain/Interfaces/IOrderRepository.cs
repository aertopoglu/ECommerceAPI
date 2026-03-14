using ECommerceAPI.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int  userId);
        Task<Order?> GetOrderByIdAsync(int OrderId);
        Task AddOrderItemAsync(OrderItem orderItem);
        Task<OrderItem?> GetOrderItemByIdAsync(int orderItemId);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(string OrderStatus);
        Task<int> GetOrderCountAsync(int userId);
    }
}
