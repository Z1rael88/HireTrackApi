using Application.Dtos.Resume.Education;
using Application.Dtos.Resume.Technology;
using Application.Dtos.User;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators
{
    public class TechnologyValidator : AbstractValidator<TechnologyRequestDto>
    {
        public TechnologyValidator()
        {
            RuleFor(u => u.YearsOfExperience)
                .NotEmpty().WithMessage("Years of experience is required");
            RuleFor(u => u.TechnologyTypeId)
                .NotEmpty().WithMessage("Technology type is required");
        }
    }
}