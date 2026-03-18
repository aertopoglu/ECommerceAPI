using ECommerceAPI.Core.DTOs.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Interfaces
{
    public interface IAddressReadService
    {
        Task<IEnumerable<AddressDTO>> GetAddressesByUserIdAsync(int userId);
        Task<IEnumerable<AddressDTO>> GetAddressesByAddressTitleAsync(string addressTitle); //admin
        Task<IEnumerable<AddressDTO>> GetAddressesByCityAsync(string addressCity); //admin
    }
}
