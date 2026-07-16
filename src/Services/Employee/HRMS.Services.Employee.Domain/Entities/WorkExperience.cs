namespace HRMS.Services.Employee.Domain.Entities;

public class WorkExperience : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public string CompanyName { get; private set; } = string.Empty;
    public string? Designation { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public string? Description { get; private set; }
    public bool IsCurrent { get; private set; }
    public string? ReasonForLeaving { get; private set; }
    public string? ReferenceName { get; private set; }
    public string? ReferencePhone { get; private set; }

    private WorkExperience() { }

    public static WorkExperience Create(
        Guid employeeId, string companyName, string? designation,
        DateTime? startDate, DateTime? endDate, string? description,
        bool isCurrent, string? reasonForLeaving, string? referenceName, string? referencePhone)
    {
        return new WorkExperience
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            CompanyName = companyName,
            Designation = designation,
            StartDate = startDate,
            EndDate = endDate,
            Description = description,
            IsCurrent = isCurrent,
            ReasonForLeaving = reasonForLeaving,
            ReferenceName = referenceName,
            ReferencePhone = referencePhone,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? companyName, string? designation, DateTime? startDate,
        DateTime? endDate, string? description, bool? isCurrent, string? reasonForLeaving,
        string? referenceName, string? referencePhone)
    {
        CompanyName = companyName ?? CompanyName;
        Designation = designation ?? Designation;
        if (startDate.HasValue) StartDate = startDate;
        if (endDate.HasValue) EndDate = endDate;
        Description = description ?? Description;
        if (isCurrent.HasValue) IsCurrent = isCurrent.Value;
        ReasonForLeaving = reasonForLeaving ?? ReasonForLeaving;
        ReferenceName = referenceName ?? ReferenceName;
        ReferencePhone = referencePhone ?? ReferencePhone;
        UpdatedAt = DateTime.UtcNow;
    }
}
