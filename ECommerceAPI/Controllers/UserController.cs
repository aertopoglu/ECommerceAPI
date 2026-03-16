using ECommerceAPI.Core.DTOs.User;
using ECommerceAPI.Core.Interfaces;
using ECommerceAPI.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerdto)
        {
            await _userService.RegisterAsync(registerdto);
            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO logindto)
        {
            var token = await _userService.LoginAsync(logindto);
            return Ok(new { Token = token });
        }


    }
}
