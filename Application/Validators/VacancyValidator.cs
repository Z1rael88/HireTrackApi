using Application.Dtos;
using Application.Dtos.Vacancy;
using FluentValidation;

namespace Application.Validators;

public class VacancyValidator : AbstractValidator<VacancyRequestDto>
{
    public VacancyValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("Name is required");
        RuleFor(u => u.Description)
            .NotEmpty().WithMessage("Description is required");
        RuleFor(u => u.Salary)
            .NotNull().WithMessage("Salary is required");
        RuleFor(u => u.AddDate)
            .NotEmpty()
            .WithMessage("Add date is required")
            .Must(date => date >= DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Add date cannot be in the past.");
        RuleFor(u => u.EndDate)
            .NotEmpty()
            .WithMessage("End date is required")
            .Must(date => date >= DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("End date cannot be in the past.");



    }
}