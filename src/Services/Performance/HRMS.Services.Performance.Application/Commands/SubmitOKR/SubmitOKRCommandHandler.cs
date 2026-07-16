using HRMS.Services.Performance.Application.Events;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.SubmitOKR;

public class SubmitOKRCommandHandler : IRequestHandler<SubmitOKRCommand>
{
    private readonly IPerformanceDbContext _context;

    public SubmitOKRCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(SubmitOKRCommand request, CancellationToken cancellationToken)
    {
        var okr = await _context.OKRs.FindAsync(new object[] { request.OKRId }, cancellationToken)
            ?? throw new Exception($"OKR with ID {request.OKRId} not found.");

        okr.Submit();
        okr.RaiseEvent(new OKRSubmittedEvent(okr.Id, okr.EmployeeId, okr.Period));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
