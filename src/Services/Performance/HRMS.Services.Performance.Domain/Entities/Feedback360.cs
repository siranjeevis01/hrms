using HRMS.Services.Performance.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Performance.Domain.Entities;

public class Feedback360 : AggregateRoot
{
    public Guid EmployeeId { get; private set; }
    public Guid ReviewerId { get; private set; }
    public string ReviewPeriod { get; private set; } = string.Empty;
    public FeedbackRelationship Relationship { get; private set; }
    public FeedbackStatus Status { get; private set; }
    public DateTime? SubmittedAt { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private readonly List<FeedbackAnswer> _answers = new();
    public IReadOnlyCollection<FeedbackAnswer> Answers => _answers.AsReadOnly();

    private Feedback360() { }

    public static Feedback360 Create(
        Guid employeeId,
        Guid reviewerId,
        string reviewPeriod,
        FeedbackRelationship relationship,
        string tenantId)
    {
        return new Feedback360
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            ReviewerId = reviewerId,
            ReviewPeriod = reviewPeriod,
            Relationship = relationship,
            Status = FeedbackStatus.Pending,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Submit()
    {
        Status = FeedbackStatus.Submitted;
        SubmittedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        Status = FeedbackStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    internal void AddAnswer(FeedbackAnswer answer)
    {
        _answers.Add(answer);
    }
}
