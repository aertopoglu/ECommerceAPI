using ECommerceAPI.Core.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartDTO>> GetAllCartDtos();
        Task<int> GetCartCountAsync(int userId);
        Task AddToCartAsync(CreateCartDTO cartdto, int userId);
        Task UpdateCartAsync(UpdateCartDTO cartdto, int userId);
        Task DeleteFromCartAsync(int productId, int userId);
    }
}
