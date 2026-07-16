namespace HRMS.Services.Recruitment.Application.DTOs;

public class RecruitmentStatsDto
{
    public int TotalJobs { get; set; }
    public int ActiveJobs { get; set; }
    public int TotalApplications { get; set; }
    public int TotalInterviews { get; set; }
    public int TotalOffers { get; set; }
    public int TotalHires { get; set; }
    public int PendingApplications { get; set; }
    public int ScheduledInterviews { get; set; }
}
