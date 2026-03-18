using ECommerceAPI.Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Interfaces
{
    public interface IUserReadService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(int id);
        Task<UserDTO?> GetUserByEmailAsync(string email);
        Task<int> GetTotalUserCountAsync();
        Task<IEnumerable<UserDTO>> GetUsersByRoleAsync(string role);
    }
}
