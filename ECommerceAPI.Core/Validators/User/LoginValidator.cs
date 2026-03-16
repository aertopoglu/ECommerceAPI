using ECommerceAPI.Core.DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Validators.User
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email can not be blank")
                .EmailAddress().WithMessage("Email should be in proper email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password can not be blank");
        }
    }
}
