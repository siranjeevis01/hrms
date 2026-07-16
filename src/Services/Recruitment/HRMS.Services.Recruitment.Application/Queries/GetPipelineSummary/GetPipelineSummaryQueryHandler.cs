using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Interfaces;
using HRMS.Services.Recruitment.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Queries.GetPipelineSummary;

public class GetPipelineSummaryQueryHandler : IRequestHandler<GetPipelineSummaryQuery, List<PipelineSummaryDto>>
{
    private readonly IRecruitmentDbContext _context;

    public GetPipelineSummaryQueryHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<List<PipelineSummaryDto>> Handle(GetPipelineSummaryQuery request, CancellationToken cancellationToken)
    {
        var query = _context.JobApplications.AsQueryable();

        if (request.JobPostingId.HasValue)
            query = query.Where(a => a.JobPostingId == request.JobPostingId.Value);

        if (request.TenantId.HasValue)
            query = query.Where(a => a.TenantId == request.TenantId.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var grouped = await query
            .GroupBy(a => a.Status)
            .Select(g => new PipelineSummaryDto
            {
                Status = g.Key.ToString(),
                Count = g.Count(),
                Percentage = totalCount > 0 ? Math.Round((decimal)g.Count() / totalCount * 100, 1) : 0
            })
            .ToListAsync(cancellationToken);

        return grouped;
    }
}
