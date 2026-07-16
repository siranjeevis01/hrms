using FluentValidation;

namespace HRMS.Services.Chat.Application.Commands.CreateConversation;

public class CreateConversationCommandValidator : AbstractValidator<CreateConversationCommand>
{
    public CreateConversationCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Conversation name is required.")
            .MaximumLength(200).WithMessage("Conversation name must not exceed 200 characters.");

        RuleFor(x => x.CreatedBy)
            .NotEmpty().WithMessage("CreatedBy is required.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("TenantId is required.");
    }
}
