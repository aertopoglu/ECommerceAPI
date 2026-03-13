using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entites
{
    public class Address
    {
        public int AddressID { get; set; }
        public int UserID { get; set; } 
        public User User { get; set; } = null!;
        public string Title {  get; set; } = string.Empty;
        public string FullAddress {  get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District {  get; set; } = string.Empty;

        public List<Order> Orders { get; set; } = null!;

    }
}
