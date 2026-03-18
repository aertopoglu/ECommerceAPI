using ECommerceAPI.Core.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Interfaces
{
    public interface IOrderWriteService
    {
        Task CreateOrderAsync(CreateOrderDTO dto, int userId);
        Task UpdateOrderStatusAsync(UpdateOrderDTO dto);
        Task DeleteOrderAsync(int id);
    }
}
