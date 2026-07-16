using FluentValidation;

namespace HRMS.Services.Training.Application.Commands.EnrollEmployee;

public class EnrollEmployeeCommandValidator : AbstractValidator<EnrollEmployeeCommand>
{
    public EnrollEmployeeCommandValidator()
    {
        RuleFor(v => v.CourseId)
            .NotEmpty().WithMessage("CourseId is required.");

        RuleFor(v => v.EmployeeId)
            .NotEmpty().WithMessage("EmployeeId is required.");

        RuleFor(v => v.TenantId)
            .NotEmpty().WithMessage("TenantId is required.");
    }
}
