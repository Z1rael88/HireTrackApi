using Application.Dtos.Resume;
using Application.Dtos.Resume.JobExperience;
using FluentValidation;

namespace Application.Validators
{
    public class JobExperienceValidator : AbstractValidator<JobExperienceRequestDto>
    {
        public JobExperienceValidator()
        {
            RuleFor(u => u.NameOfCompany)
                .NotEmpty().WithMessage("Name of company is required");
            RuleFor(u => u.Description)
                .NotEmpty().WithMessage("Description is required");
            RuleForEach(u => u.Technologies).SetValidator(new TechnologyValidator());
            RuleFor(u => u.StartDate)
                .NotEmpty()
                .WithMessage("Start date is required")
                .Must(date => date <= DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Start date cannot be in the future.");
            RuleFor(u => u.EndDate)
                .NotEmpty()
                .WithMessage("End date is required");
        }
    }
}