using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Commands.CreateShift;

public record CreateShiftCommand : IRequest<ShiftDto>
{
    public Guid CompanyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public TimeOnly StartTime { get; init; }
    public TimeOnly EndTime { get; init; }
    public TimeSpan? BreakDuration { get; init; }
    public int GraceMinutes { get; init; } = 15;
    public bool IsFlexible { get; init; }
    public int MaxShifts { get; init; } = 1;
    public Guid TenantId { get; init; }
}
