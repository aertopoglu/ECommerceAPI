using ECommerceAPI.Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Interfaces
{
    public interface IUserWriteService
    {
     
        Task<string> LoginAsync(LoginDTO userdto);
        Task RegisterAsync(RegisterDTO userdto);
        Task UpdateUserAsync(UpdateUserDTO userdto,int userId);
        Task DeleteUserAsync(int id);
    }
}
