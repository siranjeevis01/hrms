using FluentValidation;

namespace HRMS.Services.Workflow.Application.Commands.StartWorkflow;

public class StartWorkflowCommandValidator : AbstractValidator<StartWorkflowCommand>
{
    public StartWorkflowCommandValidator()
    {
        RuleFor(x => x.WorkflowDefinitionId)
            .NotEmpty().WithMessage("Workflow definition ID is required.");

        RuleFor(x => x.EntityType)
            .IsInEnum().WithMessage("A valid entity type is required.");

        RuleFor(x => x.EntityId)
            .NotEmpty().WithMessage("Entity ID is required.");

        RuleFor(x => x.RequestedById)
            .NotEmpty().WithMessage("Requested by ID is required.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
