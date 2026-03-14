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
    public class AddressRepository : GenericRepository<Address>,IAddressRepository
    {
        public AddressRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Address>> GetAddressesByAddressTitleAsync(string addressTitle)
        {
            return await _context.Addresses.Where(a => a.Title == addressTitle).ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesByCityAsync(string city)
        {
            return await _context.Addresses.Where(a => a.City == city).ToListAsync();   
        }

        public async Task<IEnumerable<Address>> GetAddressesByUserIdAsync(int userId)
        {
            return await _context.Addresses
                .Include(a => a.User)
                .Where(a => a.UserID == userId)
                .ToListAsync();
        }

    }
}
