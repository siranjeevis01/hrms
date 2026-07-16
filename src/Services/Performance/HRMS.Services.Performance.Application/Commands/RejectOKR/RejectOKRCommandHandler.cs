using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.RejectOKR;

public class RejectOKRCommandHandler : IRequestHandler<RejectOKRCommand>
{
    private readonly IPerformanceDbContext _context;

    public RejectOKRCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RejectOKRCommand request, CancellationToken cancellationToken)
    {
        var okr = await _context.OKRs.FindAsync(new object[] { request.OKRId }, cancellationToken)
            ?? throw new Exception($"OKR with ID {request.OKRId} not found.");

        okr.Reject();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
