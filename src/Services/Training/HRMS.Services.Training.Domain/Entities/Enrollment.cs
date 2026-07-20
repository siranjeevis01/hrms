using HRMS.Services.Training.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Domain.Entities;

public class Enrollment : BaseEntity
{
    public Guid CourseId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public DateTime EnrolledAt { get; private set; }
    public new EnrollmentStatus Status { get; private set; }
    public double ProgressPercentage { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public Guid? CertificateId { get; private set; }

    private readonly List<LessonProgress> _lessonProgresses = new();
    public IReadOnlyCollection<LessonProgress> LessonProgresses => _lessonProgresses.AsReadOnly();

    private Enrollment() { }

    public static Enrollment Create(
        Guid courseId,
        Guid employeeId,
        Guid tenantId)
    {
        return new Enrollment
        {
            Id = Guid.NewGuid(),
            CourseId = courseId,
            EmployeeId = employeeId,
            EnrolledAt = DateTime.UtcNow,
            Status = EnrollmentStatus.Enrolled,
            ProgressPercentage = 0,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void MarkInProgress()
    {
        if (Status != EnrollmentStatus.Enrolled) return;
        Status = EnrollmentStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkCompleted()
    {
        Status = EnrollmentStatus.Completed;
        ProgressPercentage = 100;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateProgress(double percentage)
    {
        ProgressPercentage = Math.Min(100, Math.Max(0, percentage));
        if (ProgressPercentage > 0 && Status == EnrollmentStatus.Enrolled)
            Status = EnrollmentStatus.InProgress;
        if (ProgressPercentage >= 100)
        {
            Status = EnrollmentStatus.Completed;
            CompletedAt = DateTime.UtcNow;
        }
        UpdatedAt = DateTime.UtcNow;
    }

    public void Drop()
    {
        Status = EnrollmentStatus.Dropped;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignCertificate(Guid certificateId)
    {
        CertificateId = certificateId;
        UpdatedAt = DateTime.UtcNow;
    }
}
