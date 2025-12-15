using Application.Dtos.Resume;
using FluentValidation;

namespace Application.Validators
{
    public class LanguageLevelValidator : AbstractValidator<LanguageLevelDto>
    {
        public LanguageLevelValidator()
        {
            RuleFor(u => u.Language)
                .NotEmpty().WithMessage("Language is required");
            RuleFor(u => u.Level)
                .NotEmpty().WithMessage("Level is required");
        }
    }
}