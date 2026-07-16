using FluentValidation;

namespace HRMS.Services.Chat.Application.Commands.SendMessage;

public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.ConversationId)
            .NotEmpty().WithMessage("ConversationId is required.");

        RuleFor(x => x.SenderId)
            .NotEmpty().WithMessage("SenderId is required.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Message content is required.")
            .MaximumLength(4000).WithMessage("Message content must not exceed 4000 characters.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("TenantId is required.");
    }
}
