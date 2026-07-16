using HRMS.Services.Report.Application.DTOs;
using HRMS.Services.Report.Domain.Enums;
using MediatR;

namespace HRMS.Services.Report.Application.Queries.GetReportTemplates;

public class GetReportTemplatesQuery : IRequest<PagedReportResult<ReportTemplateDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public ReportCategory? Category { get; set; }
    public ReportType? ReportType { get; set; }
    public string? SearchTerm { get; set; }
    public string TenantId { get; set; } = string.Empty;
}

public class PagedReportResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}
