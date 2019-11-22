using System;
using FluentValidation;
using FullStack.BLL.Models;

namespace FullStack.BLL.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(u => u.Email)
               .NotEmpty().WithMessage("The email cannot be empty")
               .EmailAddress().WithMessage("This is not a valid email");

            //RuleFor(u => u.Email).Must(log)
            //    .NotEmpty().WithMessage("The email cannot be empty")
            //    .EmailAddress().WithMessage("This is not a valid email");

            RuleFor(u => u.RolesList)
                .NotEmpty().WithMessage("The role list cannot be empty");
        }

        //private bool log(string arg)
        //{
        //    throw new NotImplementedException();
        //}
    }
}