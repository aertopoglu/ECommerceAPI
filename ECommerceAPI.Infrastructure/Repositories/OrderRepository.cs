using ECommerceAPI.Domain.Entites;
using ECommerceAPI.Domain.Interfaces;
using ECommerceAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>,IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Order?> GetOrderByIdAsync(int OrderId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.Address)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.OrderID == OrderId);
        }

        public async Task<int> GetOrderCountAsync(int userId)
        {
            return await _context.Orders.Where(o =>  userId == o.OrderID).CountAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string OrderStatus)
        {
            return await _context.Orders
                .Include (o => o.User)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.OrderStatus == OrderStatus)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .Where(o => o.UserID == userId)
                .ToListAsync();
        }

    }
}
