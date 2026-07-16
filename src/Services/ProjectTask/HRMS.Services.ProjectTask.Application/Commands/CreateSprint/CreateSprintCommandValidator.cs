using FluentValidation;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateSprint;

public class CreateSprintCommandValidator : AbstractValidator<CreateSprintCommand>
{
    public CreateSprintCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage("Project is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Sprint name is required.")
            .MaximumLength(200).WithMessage("Sprint name must not exceed 200 characters.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date.");
    }
}
