using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.UpdateOKR;

public class UpdateOKRCommandHandler : IRequestHandler<UpdateOKRCommand>
{
    private readonly IPerformanceDbContext _context;

    public UpdateOKRCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateOKRCommand request, CancellationToken cancellationToken)
    {
        var okr = await _context.OKRs.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new Exception($"OKR with ID {request.Id} not found.");

        okr.Update(request.ManagerId, request.Period);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
