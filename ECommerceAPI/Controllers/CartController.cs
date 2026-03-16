using ECommerceAPI.Core.DTOs.Cart;
using ECommerceAPI.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCartCount()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var count = await _cartService.GetCartCountAsync(userId);
            return Ok(count);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(CreateCartDTO cartdto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _cartService.AddToCartAsync(cartdto, userId);
            return Ok("Product added to cart");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCart(UpdateCartDTO cartdto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _cartService.UpdateCartAsync(cartdto, userId);
            return Ok("Cart updated");
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteFromCart(int productId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _cartService.DeleteFromCartAsync(productId, userId);
            return Ok("Product deleted from cart");
        }
    
    }
}
