using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Application.DTOs;

public class EnrollmentDto : BaseDto
{
    public Guid CourseId { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime EnrolledAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public double ProgressPercentage { get; set; }
    public DateTime? CompletedAt { get; set; }
    public Guid? CertificateId { get; set; }
    public string? CourseTitle { get; set; }
    public string? EmployeeName { get; set; }
}
