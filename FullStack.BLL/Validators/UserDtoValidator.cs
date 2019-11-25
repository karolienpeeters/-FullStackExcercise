using FluentValidation;
using FullStack.BLL.Models;
using FullStack.DAL.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FullStack.BLL.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        private readonly IUserRepository _userRepository;
      
        public UserDtoValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
          
            RuleFor(u => u.Email)
               .NotEmpty().WithMessage("The email cannot be empty")
               .EmailAddress().WithMessage("This is not a valid email");

            RuleFor(u => u.RolesList)
                .NotEmpty().WithMessage("The role list cannot be empty");

            RuleForEach(u => u.RolesList)
                .NotEmpty().WithMessage("Role cannot be empty")
                .MustAsync(ExistingRole).WithMessage("Role {PropertyValue} does not exist");

        }

        private async Task<bool> ExistingRole(string role, CancellationToken arg2)
        {
           return await _userRepository.CheckRole(role);
        }

    }
}