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

            //RuleForEach(u => u.RolesList)
            //    .NotEmpty().WithMessage("The role list cannot be empty");
        }
    }
}