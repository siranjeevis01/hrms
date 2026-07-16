using FluentValidation;

namespace HRMS.Services.Training.Application.Commands.CreateCourse;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(v => v.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(50).WithMessage("Code must not exceed 50 characters.");

        RuleFor(v => v.Duration)
            .GreaterThan(0).WithMessage("Duration must be greater than 0.");

        RuleFor(v => v.MaxEnrollments)
            .GreaterThan(0).WithMessage("Max enrollments must be greater than 0.");

        RuleFor(v => v.TenantId)
            .NotEmpty().WithMessage("TenantId is required.");
    }
}
