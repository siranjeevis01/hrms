namespace HRMS.Services.Employee.Domain.Entities;

public class Certification : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? IssuingOrganization { get; private set; }
    public DateTime? IssueDate { get; private set; }
    public DateTime? ExpiryDate { get; private set; }
    public string? CredentialId { get; private set; }
    public string? CredentialUrl { get; private set; }
    public bool DoesNotExpire { get; private set; }

    private Certification() { }

    public static Certification Create(
        Guid employeeId, string name, string? issuingOrganization,
        DateTime? issueDate, DateTime? expiryDate, string? credentialId,
        string? credentialUrl, bool doesNotExpire)
    {
        return new Certification
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            Name = name,
            IssuingOrganization = issuingOrganization,
            IssueDate = issueDate,
            ExpiryDate = expiryDate,
            CredentialId = credentialId,
            CredentialUrl = credentialUrl,
            DoesNotExpire = doesNotExpire,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? name, string? issuingOrganization, DateTime? issueDate,
        DateTime? expiryDate, string? credentialId, string? credentialUrl, bool? doesNotExpire)
    {
        Name = name ?? Name;
        IssuingOrganization = issuingOrganization ?? IssuingOrganization;
        if (issueDate.HasValue) IssueDate = issueDate;
        if (expiryDate.HasValue) ExpiryDate = expiryDate;
        CredentialId = credentialId ?? CredentialId;
        CredentialUrl = credentialUrl ?? CredentialUrl;
        if (doesNotExpire.HasValue) DoesNotExpire = doesNotExpire.Value;
        UpdatedAt = DateTime.UtcNow;
    }
}
