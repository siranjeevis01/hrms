namespace HRMS.Services.ProjectTask.Application.DTOs;

public class ProjectStatsDto
{
    public Guid ProjectId { get; set; }
    public int TotalMembers { get; set; }
    public int TotalEpics { get; set; }
    public int TotalStories { get; set; }
    public int TotalTasks { get; set; }
    public int TotalBugs { get; set; }
    public int OpenBugs { get; set; }
    public int CompletedTasks { get; set; }
    public int InProgressTasks { get; set; }
    public int TodoTasks { get; set; }
    public decimal TotalLoggedHours { get; set; }
    public decimal ProgressPercentage { get; set; }
}
