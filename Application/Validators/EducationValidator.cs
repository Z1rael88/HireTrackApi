using Application.Dtos.Resume.Education;
using Application.Dtos.User;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators
{
    public class EducationValidator : AbstractValidator<EducationRequestDto>
    {
        public EducationValidator()
        {
            RuleFor(u => u.Title)
                .NotEmpty().WithMessage("Title is required");
            RuleFor(u => u.Description)
                .NotEmpty().WithMessage("Description is required");
            RuleFor(u => u.EducationType)
                .NotEmpty().WithMessage("Education Type is required");
            RuleFor(u => u.Degree)
                .NotEmpty().WithMessage("Degree is required");
            RuleFor(u => u.StartDate)
                .NotEmpty()
                .WithMessage("Start date is required");
            RuleFor(u => u.EndDate)
                .NotEmpty()
                .WithMessage("Add date is required");
        }
    }
}