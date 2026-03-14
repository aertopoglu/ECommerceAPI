using ECommerceAPI.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<IEnumerable<Cart>> GetCartsByUserId(int UserId);
        Task<int> GetCartCountAsync(int UserId);
        Task AddToCartAsync(int userId, int productId, int Quantity);
        Task DeleteFromCartAsync(int userId, int productId);
        Task UpdateCartAsync(int userId, int cartId,int Quantity);
    }
}
