using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Queries.GetDesignations;

public record GetDesignationsQuery : IRequest<PagedResult<DesignationDto>>
{
    public Guid? CompanyId { get; init; }
    public bool? IsActive { get; init; }
    public string? SearchTerm { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
