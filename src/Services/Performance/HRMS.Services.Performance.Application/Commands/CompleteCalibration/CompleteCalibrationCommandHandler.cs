using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CompleteCalibration;

public class CompleteCalibrationCommandHandler : IRequestHandler<CompleteCalibrationCommand>
{
    private readonly IPerformanceDbContext _context;

    public CompleteCalibrationCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CompleteCalibrationCommand request, CancellationToken cancellationToken)
    {
        var session = await _context.CalibrationSessions.FindAsync(new object[] { request.SessionId }, cancellationToken)
            ?? throw new Exception($"Calibration Session with ID {request.SessionId} not found.");

        session.Complete();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
