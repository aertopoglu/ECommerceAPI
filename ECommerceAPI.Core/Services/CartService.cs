using AutoMapper;
using ECommerceAPI.Core.DTOs.Cart;
using ECommerceAPI.Core.Interfaces;
using ECommerceAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        public CartService(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        public async Task AddToCartAsync(CreateCartDTO cartdto, int userId)
        {
            await _cartRepository.AddToCartAsync(userId,cartdto.ProductID,cartdto.Quantity);
        }

        public async Task DeleteFromCartAsync(int productId, int userId)
        {
            await _cartRepository.DeleteFromCartAsync(productId,userId);
        }

        public async Task<IEnumerable<CartDTO>> GetAllCartDtos()
        {
            var carts = await _cartRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CartDTO>>(carts);
        }

        public async Task<int> GetCartCountAsync(int userId)
        {
            return await _cartRepository.GetCartCountAsync(userId);
        }

        public async Task UpdateCartAsync(UpdateCartDTO cartdto, int userId)
        {
            await _cartRepository.UpdateCartAsync(userId, cartdto.CartId, cartdto.Quantity);
        }
    }
}
