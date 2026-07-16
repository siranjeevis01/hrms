using FluentValidation;

namespace HRMS.Services.Report.Application.Commands.CreateReportTemplate;

public class CreateReportTemplateCommandValidator : AbstractValidator<CreateReportTemplateCommand>
{
    public CreateReportTemplateCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Report name is required.")
            .MaximumLength(200).WithMessage("Report name must not exceed 200 characters.");

        RuleFor(x => x.DataSource)
            .NotEmpty().WithMessage("Data source is required.")
            .MaximumLength(256).WithMessage("Data source must not exceed 256 characters.");

        RuleFor(x => x.Format)
            .IsInEnum().WithMessage("A valid format is required.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
