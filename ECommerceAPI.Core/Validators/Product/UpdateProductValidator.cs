using ECommerceAPI.Core.DTOs.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Validators.Product
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDTO>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.ProductID)
                .NotEmpty().WithMessage("Product Id can not be blank")
                .GreaterThan(0).WithMessage("Invalid Product Id");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product Name can not be blank")
                .MaximumLength(50).WithMessage("Product Name can not be that long");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock can not be negative");

            RuleFor(x => x.CategoryIDs)
                .Must(x => x.Length > 0).WithMessage("At least one category must be selected");
        }
    }
}
