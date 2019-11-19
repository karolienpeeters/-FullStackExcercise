using FluentValidation;
using FullStack.BLL.Models;

namespace FullStack.BLL.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("The email cannot be empty")
                .EmailAddress().WithMessage("This is not a valid email");
            RuleFor(u => u.PassWord)
                .NotEmpty().WithMessage("The password cannot be empty")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least 1 in Capital Case")
                .Matches("[a-z]").WithMessage("Password must contain at least 1 Letter in Small Case")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least 1 Special Character");
        }
    }
}