using ECommerceAPI.Core.DTOs.Address;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Validators.Address
{
    public class CreateAddressValidator : AbstractValidator<CreateAddressDTO>
    {
        public CreateAddressValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title can not be blank");

            RuleFor(x => x.FullAddress)
                .NotEmpty().WithMessage("Full Address can not be blank");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City can not be blank");

            RuleFor(x => x.District)
                .NotEmpty().WithMessage("District can not be blank");
        }
    }
}
