using ECommerceAPI.Core.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Interfaces
{
    public interface ICartReadService
    {
        Task<IEnumerable<CartDTO>> GetAllCartDtos();
        Task<int> GetCartCountAsync(int userId);
    }
}
