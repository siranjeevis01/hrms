using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Commands.CreateHoliday;

public record CreateHolidayCommand : IRequest<HolidayDto>
{
    public Guid CompanyId { get; init; }
    public Guid? BranchId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime Date { get; init; }
    public string Type { get; init; } = "Public";
    public bool IsRecurring { get; init; }
    public string? ApplicableDepartmentIdsJson { get; init; }
    public Guid TenantId { get; init; }
}
