namespace HRMS.Services.Training.Application.Events;

public class CourseCompletedEvent
{
    public Guid EnrollmentId { get; }
    public Guid CourseId { get; }
    public Guid EmployeeId { get; }
    public DateTime CompletedAt { get; }

    public CourseCompletedEvent(Guid enrollmentId, Guid courseId, Guid employeeId)
    {
        EnrollmentId = enrollmentId;
        CourseId = courseId;
        EmployeeId = employeeId;
        CompletedAt = DateTime.UtcNow;
    }
}
