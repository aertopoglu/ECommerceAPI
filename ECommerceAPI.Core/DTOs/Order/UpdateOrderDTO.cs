using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.DTOs.Order
{
    public class UpdateOrderDTO
    {
        public int OrderID { get; set; }

        public string OrderStatus { get; set; } = string.Empty;

    }
}
