using FluentValidation;

namespace HRMS.Services.Performance.Application.Commands.CreateAppraisal;

public class CreateAppraisalCommandValidator : AbstractValidator<CreateAppraisalCommand>
{
    public CreateAppraisalCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("Employee ID is required.");

        RuleFor(x => x.Period)
            .NotEmpty().WithMessage("Period is required.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
