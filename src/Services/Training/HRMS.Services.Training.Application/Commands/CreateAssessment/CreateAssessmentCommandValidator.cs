using FluentValidation;

namespace HRMS.Services.Training.Application.Commands.CreateAssessment;

public class CreateAssessmentCommandValidator : AbstractValidator<CreateAssessmentCommand>
{
    public CreateAssessmentCommandValidator()
    {
        RuleFor(v => v.CourseId)
            .NotEmpty().WithMessage("CourseId is required.");

        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(v => v.PassingScore)
            .GreaterThanOrEqualTo(0).WithMessage("Passing score must be greater than or equal to 0.");

        RuleFor(v => v.TotalPoints)
            .GreaterThan(0).WithMessage("Total points must be greater than 0.");

        RuleFor(v => v.MaxAttempts)
            .GreaterThan(0).WithMessage("Max attempts must be greater than 0.");

        RuleFor(v => v.TenantId)
            .NotEmpty().WithMessage("TenantId is required.");
    }
}
