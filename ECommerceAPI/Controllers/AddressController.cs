using ECommerceAPI.Core.DTOs.Address;
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
    public class AddressController : ControllerBase
    {
        private readonly IAddressWriteService _addressWriteService;
        private readonly IAddressReadService _addressReadService;

        public AddressController(IAddressWriteService addressWriteService,IAddressReadService addressReadService)
        {
            _addressWriteService = addressWriteService;
            _addressReadService = addressReadService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyAddresses()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var addresses = await _addressReadService.GetAddressesByUserIdAsync(userId);
            return Ok(addresses);
        }

        [HttpGet("city/{city}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByCity(string city)
        {
            var addresses = await _addressReadService.GetAddressesByCityAsync(city);
            return Ok(addresses);
        }

        [HttpGet("title/{title}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var addresses = await _addressReadService.GetAddressesByAddressTitleAsync(title);
            return Ok(addresses);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAddressDTO dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _addressWriteService.CreateAddressAsync(dto, userId);
            return Ok("Address created");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateAddressDTO dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _addressWriteService.UpdateAddressAsync(dto, userId);
            return Ok("Address updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _addressWriteService.DeleteAddressAsync(id, userId);
            return Ok("Address deleted successfully!");
        }
    }
}

