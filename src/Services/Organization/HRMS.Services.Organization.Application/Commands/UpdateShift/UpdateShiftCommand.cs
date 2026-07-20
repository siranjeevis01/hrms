using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Commands.UpdateShift;

public record UpdateShiftCommand : IRequest<ShiftDto>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Code { get; init; }
    public TimeOnly? StartTime { get; init; }
    public TimeOnly? EndTime { get; init; }
    public TimeSpan? BreakDuration { get; init; }
    public int? GraceMinutes { get; init; }
    public bool? IsFlexible { get; init; }
    public int? MaxShifts { get; init; }
}
