using FluentValidation;

namespace HRMS.Services.Helpdesk.Application.Commands.CreateKnowledgeArticle;

public class CreateKnowledgeArticleCommandValidator : AbstractValidator<CreateKnowledgeArticleCommand>
{
    public CreateKnowledgeArticleCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.");

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("Author ID is required.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
