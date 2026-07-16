using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.Performance.Domain.Entities;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.AddCalibrationEntry;

public class AddCalibrationEntryCommandHandler : IRequestHandler<AddCalibrationEntryCommand, Guid>
{
    private readonly IPerformanceDbContext _context;

    public AddCalibrationEntryCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddCalibrationEntryCommand request, CancellationToken cancellationToken)
    {
        var entry = CalibrationEntry.Create(
            request.CalibrationSessionId,
            request.EmployeeId,
            request.OriginalRating,
            request.CalibratedRating,
            request.Justification,
            request.TenantId);

        _context.CalibrationEntries.Add(entry);
        await _context.SaveChangesAsync(cancellationToken);

        return entry.Id;
    }
}
