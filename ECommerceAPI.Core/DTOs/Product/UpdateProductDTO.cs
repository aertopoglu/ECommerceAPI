using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.DTOs.Product
{
    public class UpdateProductDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string URL { get; set; } = string.Empty;
        public int Stock { get; set; }
        public bool IsHome { get; set; }
        public bool IsApproved { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public int[] CategoryIDs { get; set; } = Array.Empty<int>();

    }
}
