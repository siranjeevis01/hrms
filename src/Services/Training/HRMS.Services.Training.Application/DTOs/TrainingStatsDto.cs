namespace HRMS.Services.Training.Application.DTOs;

public class TrainingStatsDto
{
    public int TotalCourses { get; set; }
    public int PublishedCourses { get; set; }
    public int TotalEnrollments { get; set; }
    public int ActiveEnrollments { get; set; }
    public int CompletedEnrollments { get; set; }
    public int TotalAssessments { get; set; }
    public int CertificatesIssued { get; set; }
    public double AverageCompletionRate { get; set; }
    public double AverageProgressPercentage { get; set; }
}
