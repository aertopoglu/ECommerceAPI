using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.DTOs.Address
{
    public class UpdateAddressDTO
    {
        public int AddressID { get; set; }
            
        public string Title { get; set; } = string.Empty;

        public string FullAddress { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string District { get; set; } = string.Empty;
    }
}
