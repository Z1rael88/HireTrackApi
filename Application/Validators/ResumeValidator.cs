using Application.Dtos.Resume;
using FluentValidation;

namespace Application.Validators
{
    public class ResumeValidator : AbstractValidator<ResumeRequestDto>
    {
        public ResumeValidator()
        {
            RuleFor(u => u.ExpectedSalary)
                .NotEmpty().WithMessage("Expected Salary is required");
            RuleFor(u => u.YearsOfExperience)
                .NotEmpty().WithMessage("Years of experience is required");
            RuleFor(u => u.Candidate.Firstname)
                .NotEmpty().WithMessage("First name is required");
            RuleFor(u => u.Candidate.Firstname)
                .NotEmpty().WithMessage("Last name is required");
            RuleFor(u => u.Candidate.Age)
                .NotEmpty().WithMessage("Age is required");
            RuleFor(u => u.Candidate.Email)
                .NotEmpty().WithMessage("Email is required");
            RuleFor(u => u.Candidate.Bio)
                .NotEmpty().WithMessage("Bio is required");
            RuleFor(u => u.Candidate.WorkType)
                .NotEmpty().WithMessage("Bio is required");
            RuleFor(u => u.Candidate.Address)
                .NotEmpty().WithMessage("Address is required");
            RuleFor(u => u.Educations)
                .NotEmpty().WithMessage("Educations is required");
            RuleFor(u => u.JobExperiences)
                .NotEmpty().WithMessage("Job Experience is required");
            RuleFor(u => u.LanguageLevels)
                .NotEmpty().WithMessage("Language Level is required");
            RuleForEach(u => u.Educations).SetValidator(new EducationValidator());
            RuleForEach(u => u.LanguageLevels).SetValidator(new LanguageLevelValidator());
            RuleForEach(u => u.JobExperiences).SetValidator(new JobExperienceValidator());
        }
    }
}