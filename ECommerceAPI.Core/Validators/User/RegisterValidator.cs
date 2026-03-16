using ECommerceAPI.Core.DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Validators.User
{
    public class RegisterValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name can not be blank")
                .MaximumLength(20).WithMessage("First Name can not be that long");

            RuleFor(x => x.LastName)
                 .NotEmpty().WithMessage("Last Name can not be blank")
                 .MaximumLength(20).WithMessage("Last Name can not be that long");

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email can not be blank")
               .EmailAddress().WithMessage("Email should be be in proper email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password can not be blank")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number");

            RuleFor(x => x.ConfirmPassword)
               .NotEmpty().WithMessage("Confirm Password can not be blank")
               .Equal(x => x.Password).WithMessage("Passwords do not match");

        }
    }
}
