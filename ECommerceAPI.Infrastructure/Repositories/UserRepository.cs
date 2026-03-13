using ECommerceAPI.Domain.Entites;
using ECommerceAPI.Domain.Interfaces;
using ECommerceAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>,IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

       
        public async Task<User?> GetUserByEmailAsync(string Email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string Role)
        {
            return await _context.Users.Where(u => u.Role == Role).ToListAsync();
        }

        public async Task<int> TotalUserCountAsync()
        {
            return await _context.Users.CountAsync();
        }

    }
}
