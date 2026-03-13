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
    public class CartRepository : GenericRepository<Cart>,ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddToCartAsync(int userId, int productId, int Quantity)
        {
            var existingcart = await _context.Carts.FirstOrDefaultAsync(c => c.UserID == userId && c.ProductID == productId);
            if (existingcart != null)
            {
                existingcart.Quantity = existingcart.Quantity + Quantity;
                await _context.SaveChangesAsync();
            }
            else
            {
                await _context.Carts.AddAsync(new Cart { UserID = userId, ProductID = productId, Quantity = Quantity });

            }
        }
       
        public async Task DeleteFromCartAsync(int userId, int productId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserID == userId && c.ProductID == productId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetCartCountAsync(int UserId)
        {
            return await _context.Carts.Where(c => c.UserID == UserId).CountAsync();
        }

        public async Task<IEnumerable<Cart>> GetCartsByUserId(int UserId)
        {
            return await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserID == UserId)
                .ToListAsync();
        }

        public async Task UpdateCartAsync(int userId, int productId, int Quantity)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserID == userId && c.ProductID==productId);
            if(cart != null)
            {
                cart.Quantity = Quantity;
                _context.SaveChanges();
            }
        }
    }
}
