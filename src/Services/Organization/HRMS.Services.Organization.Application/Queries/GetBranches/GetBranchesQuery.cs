using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Queries.GetBranches;

public record GetBranchesQuery : IRequest<PagedResult<BranchDto>>
{
    public Guid? CompanyId { get; init; }
    public bool? IsActive { get; init; }
    public string? SearchTerm { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SortBy { get; init; }
    public bool SortDescending { get; init; }
}
