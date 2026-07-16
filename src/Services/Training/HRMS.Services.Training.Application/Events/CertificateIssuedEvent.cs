namespace HRMS.Services.Training.Application.Events;

public class CertificateIssuedEvent
{
    public Guid CertificateId { get; }
    public Guid CourseId { get; }
    public Guid EmployeeId { get; }
    public string CertificateNumber { get; }
    public DateTime IssuedAt { get; }

    public CertificateIssuedEvent(Guid certificateId, Guid courseId, Guid employeeId, string certificateNumber)
    {
        CertificateId = certificateId;
        CourseId = courseId;
        EmployeeId = employeeId;
        CertificateNumber = certificateNumber;
        IssuedAt = DateTime.UtcNow;
    }
}
