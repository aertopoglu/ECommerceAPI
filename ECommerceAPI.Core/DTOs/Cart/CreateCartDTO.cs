using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.DTOs.Cart
{
    public class CreateCartDTO
    {
        public int ProductID { get; set; }

        public int Quantity { get; set; }
    }
}
