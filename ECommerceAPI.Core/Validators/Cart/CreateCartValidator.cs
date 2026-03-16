using ECommerceAPI.Core.DTOs.Cart;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Validators.Cart
{
    public class CreateCartValidator : AbstractValidator<CreateCartDTO>
    {
        public CreateCartValidator()
        {
            RuleFor(x => x.ProductID)
                .GreaterThan(0).WithMessage("Invalid Product Id");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity can not be empty")
                .GreaterThan(0).WithMessage("Quantity must be at least 1");
        }
    }
}
