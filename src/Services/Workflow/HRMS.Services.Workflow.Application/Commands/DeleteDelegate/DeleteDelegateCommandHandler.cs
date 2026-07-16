using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Commands.DeleteDelegate;

public class DeleteDelegateCommandHandler : IRequestHandler<DeleteDelegateCommand>
{
    private readonly IWorkflowDbContext _context;

    public DeleteDelegateCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteDelegateCommand request, CancellationToken cancellationToken)
    {
        var delegateEntity = await _context.Delegates
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (delegateEntity == null)
            throw new InvalidOperationException($"Delegate with ID {request.Id} not found.");

        delegateEntity.Deactivate();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
