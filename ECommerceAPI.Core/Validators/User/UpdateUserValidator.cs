using ECommerceAPI.Core.DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Validators.User
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name can not be blank")
                .MaximumLength(20).WithMessage("First Name can not be that long");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name can not be blank")
                .MaximumLength(20).WithMessage("Last Name can not be that long");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email can not be blank")
                .EmailAddress().WithMessage("Invalid email format");
        }
    }
}
