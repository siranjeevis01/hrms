namespace HRMS.Services.Training.Application.Events;

public class EnrollmentCreatedEvent
{
    public Guid EnrollmentId { get; }
    public Guid CourseId { get; }
    public Guid EmployeeId { get; }
    public DateTime EnrolledAt { get; }

    public EnrollmentCreatedEvent(Guid enrollmentId, Guid courseId, Guid employeeId)
    {
        EnrollmentId = enrollmentId;
        CourseId = courseId;
        EmployeeId = employeeId;
        EnrolledAt = DateTime.UtcNow;
    }
}
