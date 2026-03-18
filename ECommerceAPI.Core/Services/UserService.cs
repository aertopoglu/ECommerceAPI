using AutoMapper;
using ECommerceAPI.Core.DTOs.User;
using ECommerceAPI.Core.Interfaces;
using ECommerceAPI.Domain.Entites;
using ECommerceAPI.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Services
{
    public class UserService : IUserWriteService,IUserReadService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<int> GetTotalUserCountAsync()
        {
           return await _userRepository.TotalUserCountAsync();
        }

        public async Task<UserDTO?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            return _mapper.Map<UserDTO?>(user);
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDTO?>(user);
        }

        public async Task<IEnumerable<UserDTO>> GetUsersByRoleAsync(string role)
        {
            var users = await _userRepository.GetUsersByRoleAsync(role);
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<string> LoginAsync(LoginDTO userdto)
        {
            var user = await _userRepository.GetUserByEmailAsync(userdto.Email);
            if (user == null) throw new Exception("Email or Password is wrong,pls try again");

            if (!BCrypt.Net.BCrypt.Verify(userdto.Password, user.PasswordHash)) throw new Exception("Email or Password is wrong,pls try again");

            return GenerateJwtToken(user);
        }

        public async Task RegisterAsync(RegisterDTO userdto)
        {
            var existinguser = await _userRepository.GetUserByEmailAsync(userdto.Email);
            if (existinguser != null) throw new Exception("This email already registered");

            var user = _mapper.Map<User>(userdto);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userdto.Password);

            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(UpdateUserDTO userdto, int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) throw new Exception("User not found");
            _mapper.Map(userdto, user);
            await _userRepository.UpdateAsync(user);
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(
                    int.Parse(_configuration["JwtSettings:ExpirationInDays"]!)),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
