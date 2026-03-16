using ECommerceAPI.Core.DTOs.Order;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Validators.Order
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderDTO>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.OrderID)
                .GreaterThan(0).WithMessage("Invalid Order ID");

            RuleFor(x => x.OrderStatus)
                .NotEmpty().WithMessage("Order Status can not be blank")
                .Must(x => x == "Pending" || x == "Shipped" || x == "Delivered" || x == "Cancelled").WithMessage("Invalid Order Status");
        }

    }
}
