using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Queries.GetShifts;

public record GetShiftsQuery : IRequest<PagedResult<ShiftDto>>
{
    public Guid? CompanyId { get; init; }
    public bool? IsActive { get; init; }
    public string? SearchTerm { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
