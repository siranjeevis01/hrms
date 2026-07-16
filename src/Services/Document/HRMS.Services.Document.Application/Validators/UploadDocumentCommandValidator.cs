using FluentValidation;
using HRMS.Services.Document.Application.Commands.UploadDocument;

namespace HRMS.Services.Document.Application.Validators;

public class UploadDocumentCommandValidator : AbstractValidator<UploadDocumentCommand>
{
    public UploadDocumentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Document name is required.")
            .MaximumLength(200).WithMessage("Document name must not exceed 200 characters.");

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage("File name is required.")
            .MaximumLength(256).WithMessage("File name must not exceed 256 characters.");

        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage("Content type is required.")
            .MaximumLength(100).WithMessage("Content type must not exceed 100 characters.");

        RuleFor(x => x.FileSize)
            .GreaterThan(0).WithMessage("File size must be greater than 0.");

        RuleFor(x => x.FileUrl)
            .NotEmpty().WithMessage("File URL is required.")
            .MaximumLength(1000).WithMessage("File URL must not exceed 1000 characters.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
