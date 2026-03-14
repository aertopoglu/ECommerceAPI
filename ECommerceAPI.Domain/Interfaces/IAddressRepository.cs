using ECommerceAPI.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Interfaces
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<IEnumerable<Address>> GetAddressesByUserIdAsync(int userId);
        Task<IEnumerable<Address>> GetAddressesByAddressTitleAsync(string addressTitle);
        Task<IEnumerable<Address>> GetAddressesByCityAsync(string city);
    }
}
