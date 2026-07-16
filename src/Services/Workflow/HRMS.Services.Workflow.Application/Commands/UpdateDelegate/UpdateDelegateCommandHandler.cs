using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Commands.UpdateDelegate;

public class UpdateDelegateCommandHandler : IRequestHandler<UpdateDelegateCommand>
{
    private readonly IWorkflowDbContext _context;

    public UpdateDelegateCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateDelegateCommand request, CancellationToken cancellationToken)
    {
        var delegateEntity = await _context.Delegates
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (delegateEntity == null)
            throw new InvalidOperationException($"Delegate with ID {request.Id} not found.");

        delegateEntity.Update(request.DelegateToUserId, request.StartDate, request.EndDate, request.EntityType);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
