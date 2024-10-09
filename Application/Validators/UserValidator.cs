using Application.Dtos.User;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators
{
    public class UserValidator : AbstractValidator<BaseUserDto>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First name is required");
            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last name is required");
            RuleFor(u => u.Age)
                .NotEmpty().WithMessage("Age is required")
                .Must(age => age >= 18 && age <= 100)
                .WithMessage("Age must be between 18 and 100.");
            RuleFor(u => u.Role)
                .NotEmpty()
                .WithMessage("Role is required")
                .Must(x => x != Role.SystemAdministrator)
                .WithMessage("Role cannot be System Administrator");
        }
    }
}