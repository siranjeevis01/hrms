using HRMS.Shared.Kernel.Common;
using HRMS.Services.Recruitment.Domain.Enums;

namespace HRMS.Services.Recruitment.Domain.Entities;

public class OfferLetter : AggregateRoot
{
    public Guid JobApplicationId { get; private set; }
    public Guid CandidateId { get; private set; }
    public Guid? EmployeeId { get; private set; }
    public string Position { get; private set; } = string.Empty;
    public Guid DepartmentId { get; private set; }
    public Guid DesignationId { get; private set; }
    public decimal CTC { get; private set; }
    public decimal BasicSalary { get; private set; }
    public DateTime JoiningDate { get; private set; }
    public DateTime OfferExpiryDate { get; private set; }
    public new OfferStatus Status { get; private set; }
    public DateTime? SentAt { get; private set; }
    public DateTime? AcceptedAt { get; private set; }
    public DateTime? RejectedAt { get; private set; }
    public string? RejectionReason { get; private set; }
    public string? DocumentUrl { get; private set; }
    public new Guid TenantId { get; private set; }

    public JobApplication? JobApplication { get; set; }
    public Candidate? Candidate { get; set; }

    private OfferLetter() { }

    public static OfferLetter Create(
        Guid jobApplicationId, Guid candidateId, string position,
        Guid departmentId, Guid designationId, decimal ctc,
        decimal basicSalary, DateTime joiningDate, DateTime offerExpiryDate,
        Guid tenantId)
    {
        return new OfferLetter
        {
            Id = Guid.NewGuid(),
            JobApplicationId = jobApplicationId,
            CandidateId = candidateId,
            Position = position,
            DepartmentId = departmentId,
            DesignationId = designationId,
            CTC = ctc,
            BasicSalary = basicSalary,
            JoiningDate = joiningDate,
            OfferExpiryDate = offerExpiryDate,
            Status = OfferStatus.Draft,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Send(string documentUrl)
    {
        Status = OfferStatus.Sent;
        DocumentUrl = documentUrl;
        SentAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Accept()
    {
        if (Status != OfferStatus.Sent)
            throw new InvalidOperationException("Only sent offers can be accepted.");
        if (DateTime.UtcNow > OfferExpiryDate)
            throw new InvalidOperationException("This offer has expired.");
        Status = OfferStatus.Accepted;
        AcceptedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reject(string? reason)
    {
        if (Status != OfferStatus.Sent)
            throw new InvalidOperationException("Only sent offers can be rejected.");
        Status = OfferStatus.Rejected;
        RejectedAt = DateTime.UtcNow;
        RejectionReason = reason;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Expire()
    {
        if (Status == OfferStatus.Sent)
        {
            Status = OfferStatus.Expired;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void SetEmployeeId(Guid employeeId)
    {
        EmployeeId = employeeId;
        UpdatedAt = DateTime.UtcNow;
    }
}
