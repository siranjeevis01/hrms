using FluentValidation;
using HRMS.Services.Document.Application.Commands.CreateFolder;

namespace HRMS.Services.Document.Application.Validators;

public class CreateFolderCommandValidator : AbstractValidator<CreateFolderCommand>
{
    public CreateFolderCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Folder name is required.")
            .MaximumLength(200).WithMessage("Folder name must not exceed 200 characters.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
