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
        private readonly IUserWriteService _userWriteService;

        public UserController(IUserWriteService userWriteService)
        {
            _userWriteService = userWriteService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerdto)
        {
            await _userWriteService.RegisterAsync(registerdto);
            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO logindto)
        {
            var token = await _userWriteService.LoginAsync(logindto);
            return Ok(new { Token = token });
        }


    }
}
