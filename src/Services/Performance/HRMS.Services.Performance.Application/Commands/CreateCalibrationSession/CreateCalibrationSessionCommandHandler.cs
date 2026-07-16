using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.Performance.Domain.Entities;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CreateCalibrationSession;

public class CreateCalibrationSessionCommandHandler : IRequestHandler<CreateCalibrationSessionCommand, Guid>
{
    private readonly IPerformanceDbContext _context;

    public CreateCalibrationSessionCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateCalibrationSessionCommand request, CancellationToken cancellationToken)
    {
        var session = CalibrationSession.Create(
            request.Name,
            request.Description,
            request.ReviewPeriod,
            request.ConductedBy,
            request.TenantId);

        _context.CalibrationSessions.Add(session);
        await _context.SaveChangesAsync(cancellationToken);

        return session.Id;
    }
}
