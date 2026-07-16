using HRMS.Services.Report.Application.DTOs;
using HRMS.Services.Report.Application.Interfaces;
using HRMS.Services.Report.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Report.Application.Queries.GetReportCategories;

public class GetReportCategoriesQueryHandler : IRequestHandler<GetReportCategoriesQuery, List<ReportCategoryDto>>
{
    private readonly IReportDbContext _context;

    public GetReportCategoriesQueryHandler(IReportDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReportCategoryDto>> Handle(GetReportCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.ReportTemplates
            .Where(t => t.TenantId == request.TenantId)
            .GroupBy(t => t.Category)
            .Select(g => new ReportCategoryDto
            {
                Category = g.Key,
                ReportCount = g.Count()
            })
            .OrderBy(c => c.Category)
            .ToListAsync(cancellationToken);

        return categories;
    }
}
