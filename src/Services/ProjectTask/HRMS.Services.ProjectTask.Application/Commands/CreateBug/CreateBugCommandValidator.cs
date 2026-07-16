using FluentValidation;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateBug;

public class CreateBugCommandValidator : AbstractValidator<CreateBugCommand>
{
    public CreateBugCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage("Project is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Bug title is required.")
            .MaximumLength(500).WithMessage("Bug title must not exceed 500 characters.");
    }
}
