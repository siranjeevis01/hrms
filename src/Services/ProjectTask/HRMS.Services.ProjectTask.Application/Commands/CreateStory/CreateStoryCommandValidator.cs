using FluentValidation;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateStory;

public class CreateStoryCommandValidator : AbstractValidator<CreateStoryCommand>
{
    public CreateStoryCommandValidator()
    {
        RuleFor(x => x.EpicId)
            .NotEmpty().WithMessage("Epic is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Story title is required.")
            .MaximumLength(300).WithMessage("Story title must not exceed 300 characters.");
    }
}
