using HRMS.Services.Report.Application.DTOs;
using MediatR;

namespace HRMS.Services.Report.Application.Queries.GetReportCategories;

public class GetReportCategoriesQuery : IRequest<List<ReportCategoryDto>>
{
    public string TenantId { get; set; } = string.Empty;
}
