using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.Performance.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetPerformanceStats;

public class GetPerformanceStatsQueryHandler : IRequestHandler<GetPerformanceStatsQuery, PerformanceStatsDto>
{
    private readonly IPerformanceDbContext _context;

    public GetPerformanceStatsQueryHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task<PerformanceStatsDto> Handle(GetPerformanceStatsQuery request, CancellationToken cancellationToken)
    {
        var goals = _context.Goals.Where(g => g.TenantId == request.TenantId);
        var okrs = _context.OKRs.Where(o => o.TenantId == request.TenantId);
        var kpis = _context.KPIs.Where(k => k.TenantId == request.TenantId);
        var reviews = _context.PerformanceReviews.Where(r => r.TenantId == request.TenantId);
        var appraisals = _context.Appraisals.Where(a => a.TenantId == request.TenantId);

        var totalGoals = await goals.CountAsync(cancellationToken);
        var activeGoals = await goals.CountAsync(g => g.Status == GoalStatus.Active, cancellationToken);
        var completedGoals = await goals.CountAsync(g => g.Status == GoalStatus.Completed, cancellationToken);
        var overdueGoals = await goals.CountAsync(g => g.Status == GoalStatus.Overdue, cancellationToken);

        var totalOKRs = await okrs.CountAsync(cancellationToken);
        var approvedOKRs = await okrs.CountAsync(o => o.Status == OKRStatus.Approved, cancellationToken);

        var totalKPIs = await kpis.CountAsync(cancellationToken);

        var totalReviews = await reviews.CountAsync(cancellationToken);
        var completedReviews = await reviews.CountAsync(r => r.Status == ReviewStatus.Completed || r.Status == ReviewStatus.Approved, cancellationToken);

        var totalAppraisals = await appraisals.CountAsync(cancellationToken);
        var approvedAppraisals = await appraisals.CountAsync(a => a.Status == AppraisalStatus.Approved, cancellationToken);

        var avgGoalProgress = totalGoals > 0
            ? await goals.AverageAsync(g => g.TargetValue > 0 && g.CurrentValue.HasValue ? g.CurrentValue.Value / g.TargetValue.Value * 100 : 0, cancellationToken)
            : 0;

        var avgReviewScore = completedReviews > 0
            ? await reviews.Where(r => r.OverallScore.HasValue).AverageAsync(r => r.OverallScore!.Value, cancellationToken)
            : 0;

        var avgKPIScore = totalKPIs > 0
            ? await kpis.AverageAsync(k => k.TargetValue > 0 ? k.CurrentValue / k.TargetValue * 100 : 0, cancellationToken)
            : 0;

        return new PerformanceStatsDto
        {
            TotalGoals = totalGoals,
            ActiveGoals = activeGoals,
            CompletedGoals = completedGoals,
            OverdueGoals = overdueGoals,
            TotalOKRs = totalOKRs,
            ApprovedOKRs = approvedOKRs,
            TotalKPIs = totalKPIs,
            TotalReviews = totalReviews,
            CompletedReviews = completedReviews,
            TotalAppraisals = totalAppraisals,
            ApprovedAppraisals = approvedAppraisals,
            AverageGoalProgress = Math.Round(avgGoalProgress, 2),
            AverageReviewScore = Math.Round(avgReviewScore, 2),
            AverageKPIScore = Math.Round(avgKPIScore, 2)
        };
    }
}
