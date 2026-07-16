using HRMS.Services.Workflow.Application.Interfaces;
using HRMS.Services.Workflow.Domain.Entities;
using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.CreateDelegate;

public class CreateDelegateCommandHandler : IRequestHandler<CreateDelegateCommand, Guid>
{
    private readonly IWorkflowDbContext _context;

    public CreateDelegateCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateDelegateCommand request, CancellationToken cancellationToken)
    {
        var delegateEntity = Delegation.Create(
            request.UserId,
            request.DelegateToUserId,
            request.StartDate,
            request.EndDate,
            request.EntityType,
            request.TenantId);

        _context.Delegates.Add(delegateEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return delegateEntity.Id;
    }
}
