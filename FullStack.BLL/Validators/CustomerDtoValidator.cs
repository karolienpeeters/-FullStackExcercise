using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FullStack.BLL.Models;

namespace FullStack.BLL.Validators
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("The firstname cannot be empty");

            RuleFor(c => c.LastName).NotEmpty().WithMessage("The lastname cannot be empty");

        }
    }
}
