using ECommerceAPI.Core.DTOs.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Validators.Category
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category Name can not be blank")
                .MaximumLength(50).WithMessage("Category Name can not be that long");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("URL can not be blank");
        }
    }
}
