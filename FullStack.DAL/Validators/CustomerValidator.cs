using FluentValidation;
using FullStack.DAL.Models.Entities;

namespace FullStack.DAL.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.Person.FirstName).NotEmpty().WithMessage("The firstname cannot be empty");

            RuleFor(c => c.Person.LastName).NotEmpty().WithMessage("The lastname cannot be empty");
        }
    }
}