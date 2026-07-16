using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.ApproveOKR;

public class ApproveOKRCommandHandler : IRequestHandler<ApproveOKRCommand>
{
    private readonly IPerformanceDbContext _context;

    public ApproveOKRCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ApproveOKRCommand request, CancellationToken cancellationToken)
    {
        var okr = await _context.OKRs.FindAsync(new object[] { request.OKRId }, cancellationToken)
            ?? throw new Exception($"OKR with ID {request.OKRId} not found.");

        okr.Approve(request.OverallScore);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
