using HRMS.Services.Dashboard.Application.DTOs;
using HRMS.Services.Dashboard.Domain.Enums;
using MediatR;

namespace HRMS.Services.Dashboard.Application.Queries.GetDashboards;

public class GetDashboardsQuery : IRequest<PagedDashboardResult<DashboardDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? UserId { get; set; }
    public DashboardCategory? Category { get; set; }
    public string? SearchTerm { get; set; }
    public string TenantId { get; set; } = string.Empty;
}

public class PagedDashboardResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}
