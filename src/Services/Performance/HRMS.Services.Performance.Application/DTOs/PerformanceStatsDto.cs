namespace HRMS.Services.Performance.Application.DTOs;

public class PerformanceStatsDto
{
    public int TotalGoals { get; set; }
    public int ActiveGoals { get; set; }
    public int CompletedGoals { get; set; }
    public int OverdueGoals { get; set; }
    public int TotalOKRs { get; set; }
    public int ApprovedOKRs { get; set; }
    public int TotalKPIs { get; set; }
    public int TotalReviews { get; set; }
    public int CompletedReviews { get; set; }
    public int TotalAppraisals { get; set; }
    public int ApprovedAppraisals { get; set; }
    public decimal AverageGoalProgress { get; set; }
    public decimal AverageReviewScore { get; set; }
    public decimal AverageKPIScore { get; set; }
}
