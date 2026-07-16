namespace HRMS.Services.Training.Application.DTOs;

public class CourseAnalyticsDto
{
    public Guid CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public int TotalEnrollments { get; set; }
    public int ActiveEnrollments { get; set; }
    public int CompletedEnrollments { get; set; }
    public int DroppedEnrollments { get; set; }
    public double CompletionRate { get; set; }
    public double AverageProgress { get; set; }
    public double AverageRating { get; set; }
    public int CertificatesIssued { get; set; }
}
