using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Domain.Enums;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetPerformanceReviews;

public class GetPerformanceReviewsQuery : IRequest<PagedResult<PerformanceReviewDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? EmployeeId { get; set; }
    public ReviewType? ReviewType { get; set; }
    public ReviewStatus? Status { get; set; }
    public string? ReviewPeriod { get; set; }
    public string TenantId { get; set; } = string.Empty;
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}
