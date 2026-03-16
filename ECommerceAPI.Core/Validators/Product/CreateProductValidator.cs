using ECommerceAPI.Core.DTOs.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Validators.Product
{
    public class CreateProductValidator : AbstractValidator<CreateProductDTO>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product Name can not be blank")
                .MaximumLength(50).WithMessage("Product Name can not be that long");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description can not be blank")
                .MaximumLength(100).WithMessage("Description can not be that long");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price can not be empty")
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.URL)
                .NotEmpty().WithMessage("URL can not be blank");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock can not be negative");

            RuleFor(x => x.CategoryIDs)
                .NotNull().WithMessage("At least one category must be selected")
                .Must(x => x.Length > 0).WithMessage("At least one category must be selected");
        }
    }
}
