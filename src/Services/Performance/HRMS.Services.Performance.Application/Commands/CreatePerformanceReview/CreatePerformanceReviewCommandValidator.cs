using FluentValidation;

namespace HRMS.Services.Performance.Application.Commands.CreatePerformanceReview;

public class CreatePerformanceReviewCommandValidator : AbstractValidator<CreatePerformanceReviewCommand>
{
    public CreatePerformanceReviewCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("Employee ID is required.");

        RuleFor(x => x.ReviewerId)
            .NotEmpty().WithMessage("Reviewer ID is required.");

        RuleFor(x => x.ReviewPeriod)
            .NotEmpty().WithMessage("Review period is required.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
