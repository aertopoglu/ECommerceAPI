using ECommerceAPI.Core.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Interfaces
{
    public interface IOrderReadService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(int userId);
        Task<OrderDTO?> GetOrderByIdAsync(int id);
        Task<OrderItemDTO?> GetOrderItemByIdAsync(int ProductId);
        Task<int> GetOrderCountAsync(int userId);
        Task<IEnumerable<OrderDTO>> GetOrdersByStatusAsync(string status);
    }
}
