using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Commands.UpdateHoliday;

public record UpdateHolidayCommand : IRequest<HolidayDto>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public DateTime? Date { get; init; }
    public string? Type { get; init; }
    public Guid? BranchId { get; init; }
    public bool? IsRecurring { get; init; }
    public string? ApplicableDepartmentIdsJson { get; init; }
}
