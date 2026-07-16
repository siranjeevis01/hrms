using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Domain.Enums;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetAppraisals;

public class GetAppraisalsQuery : IRequest<PagedResult<AppraisalDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public AppraisalStatus? Status { get; set; }
    public AppraisalType? Type { get; set; }
    public string? Period { get; set; }
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
