namespace HRMS.Services.Performance.Application.DTOs;

public class EmployeePerformanceSummaryDto
{
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public int ActiveGoals { get; set; }
    public int CompletedGoals { get; set; }
    public decimal AverageGoalProgress { get; set; }
    public decimal? LatestOKRScore { get; set; }
    public string LatestOKRPeriod { get; set; } = string.Empty;
    public decimal? AverageKPIScore { get; set; }
    public decimal? LatestReviewScore { get; set; }
    public string LatestReviewPeriod { get; set; } = string.Empty;
    public decimal? LatestAppraisalRating { get; set; }
    public string LatestAppraisalPeriod { get; set; } = string.Empty;
    public decimal? HikePercentage { get; set; }
    public bool PromotionRecommended { get; set; }
    public List<GoalDto> RecentGoals { get; set; } = new();
    public List<KPIDto> KPIs { get; set; } = new();
}
