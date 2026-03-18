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
        private readonly ICartWriteService _cartWriteService;
        private readonly ICartReadService _cartReadService;


        public CartController(ICartWriteService cartWriteService,ICartReadService cartReadService)
        {
            _cartWriteService = cartWriteService;
            _cartReadService = cartReadService;
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCartCount()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var count = await _cartReadService.GetCartCountAsync(userId);
            return Ok(count);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(CreateCartDTO cartdto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _cartWriteService.AddToCartAsync(cartdto, userId);
            return Ok("Product added to cart");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCart(UpdateCartDTO cartdto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _cartWriteService.UpdateCartAsync(cartdto, userId);
            return Ok("Cart updated");
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteFromCart(int productId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _cartWriteService.DeleteFromCartAsync(productId, userId);
            return Ok("Product deleted from cart");
        }
    
    }
}
