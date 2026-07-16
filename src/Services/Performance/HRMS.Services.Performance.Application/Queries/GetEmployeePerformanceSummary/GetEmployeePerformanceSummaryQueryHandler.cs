using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.Performance.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetEmployeePerformanceSummary;

public class GetEmployeePerformanceSummaryQueryHandler : IRequestHandler<GetEmployeePerformanceSummaryQuery, EmployeePerformanceSummaryDto?>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeePerformanceSummaryQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EmployeePerformanceSummaryDto?> Handle(GetEmployeePerformanceSummaryQuery request, CancellationToken cancellationToken)
    {
        var goals = await _context.Goals
            .Where(g => g.EmployeeId == request.EmployeeId && g.TenantId == request.TenantId)
            .OrderByDescending(g => g.CreatedAt)
            .ToListAsync(cancellationToken);

        if (!goals.Any())
            return null;

        var okrs = await _context.OKRs
            .Where(o => o.EmployeeId == request.EmployeeId && o.TenantId == request.TenantId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(cancellationToken);

        var kpis = await _context.KPIs
            .Where(k => k.EmployeeId == request.EmployeeId && k.TenantId == request.TenantId)
            .ToListAsync(cancellationToken);

        var reviews = await _context.PerformanceReviews
            .Where(r => r.EmployeeId == request.EmployeeId && r.TenantId == request.TenantId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);

        var appraisals = await _context.Appraisals
            .Where(a => a.EmployeeId == request.EmployeeId && a.TenantId == request.TenantId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);

        var activeGoals = goals.Count(g => g.Status == GoalStatus.Active);
        var completedGoals = goals.Count(g => g.Status == GoalStatus.Completed);
        var avgGoalProgress = goals.Any(g => g.TargetValue > 0 && g.CurrentValue.HasValue)
            ? goals.Where(g => g.TargetValue > 0 && g.CurrentValue.HasValue)
                .Average(g => g.CurrentValue!.Value / g.TargetValue.Value * 100)
            : 0;

        var latestOKR = okrs.FirstOrDefault();
        var avgKPIScore = kpis.Any(k => k.TargetValue > 0)
            ? kpis.Average(k => k.CurrentValue / k.TargetValue * 100)
            : 0;

        var latestReview = reviews.FirstOrDefault();
        var latestAppraisal = appraisals.FirstOrDefault();

        return new EmployeePerformanceSummaryDto
        {
            EmployeeId = request.EmployeeId,
            EmployeeName = string.Empty,
            ActiveGoals = activeGoals,
            CompletedGoals = completedGoals,
            AverageGoalProgress = Math.Round(avgGoalProgress, 2),
            LatestOKRScore = latestOKR?.OverallScore,
            LatestOKRPeriod = latestOKR?.Period ?? string.Empty,
            AverageKPIScore = Math.Round(avgKPIScore, 2),
            LatestReviewScore = latestReview?.OverallScore,
            LatestReviewPeriod = latestReview?.ReviewPeriod ?? string.Empty,
            LatestAppraisalRating = latestAppraisal?.FinalRating,
            LatestAppraisalPeriod = latestAppraisal?.Period ?? string.Empty,
            HikePercentage = latestAppraisal?.HikePercentage,
            PromotionRecommended = latestAppraisal?.PromotionRecommended ?? false,
            RecentGoals = _mapper.Map<List<GoalDto>>(goals.Take(5)),
            KPIs = _mapper.Map<List<KPIDto>>(kpis)
        };
    }
}
