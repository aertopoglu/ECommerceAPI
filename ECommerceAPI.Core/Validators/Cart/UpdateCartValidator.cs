using ECommerceAPI.Core.DTOs.Cart;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Validators.Cart
{
    public class UpdateCartValidator : AbstractValidator<UpdateCartDTO>
    {
        public UpdateCartValidator()
        {
            RuleFor(x => x.CartId)
                .GreaterThan(0).WithMessage("Invalid Cart Id");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be at least 1");
        }
    }
}
