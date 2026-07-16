using FluentValidation;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateEpic;

public class CreateEpicCommandValidator : AbstractValidator<CreateEpicCommand>
{
    public CreateEpicCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage("Project is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Epic title is required.")
            .MaximumLength(300).WithMessage("Epic title must not exceed 300 characters.");
    }
}
