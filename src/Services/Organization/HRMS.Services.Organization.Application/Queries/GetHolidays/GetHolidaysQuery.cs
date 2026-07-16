using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Queries.GetHolidays;

public record GetHolidaysQuery : IRequest<List<HolidayDto>>
{
    public Guid CompanyId { get; init; }
    public int? Year { get; init; }
    public Guid? BranchId { get; init; }
    public bool? IsActive { get; init; }
}
