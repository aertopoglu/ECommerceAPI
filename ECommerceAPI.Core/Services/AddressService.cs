using AutoMapper;
using ECommerceAPI.Core.DTOs.Address;
using ECommerceAPI.Core.Interfaces;
using ECommerceAPI.Domain.Entites;
using ECommerceAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository,IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task CreateAddressAsync(CreateAddressDTO addressdto, int userId)
        {
            var address = _mapper.Map<Address>(addressdto);
            address.UserID = userId;
            await _addressRepository.AddAsync(address);
        }

        public async Task DeleteAddressAsync(int id, int userId)
        {
            var address = await _addressRepository.GetByIdAsync(id);
            if (address == null || address.UserID != userId)
                throw new Exception("Address not found");
            await _addressRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<AddressDTO>> GetAddressesByAddressTitleAsync(string addressTitle)
        {
           var addresses = await _addressRepository.GetAddressesByAddressTitleAsync(addressTitle);
           return _mapper.Map<IEnumerable<AddressDTO>>(addresses);
        }

        public async Task<IEnumerable<AddressDTO>> GetAddressesByCityAsync(string addressCity)
        {
            var addresses = await _addressRepository.GetAddressesByCityAsync(addressCity);
            return _mapper.Map<IEnumerable<AddressDTO>>(addresses);
        }

        public async Task<IEnumerable<AddressDTO>> GetAddressesByUserIdAsync(int userId)
        {
            var address = await _addressRepository.GetAddressesByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<AddressDTO>>(address);
        }

        public async Task UpdateAddressAsync(UpdateAddressDTO addressdto, int userId)
        {
            var address = await _addressRepository.GetByIdAsync(addressdto.AddressID);
            if (address == null || address.UserID != userId)
                throw new Exception("Address not found");
            await _addressRepository.UpdateAsync(address);
        }
    }
}
