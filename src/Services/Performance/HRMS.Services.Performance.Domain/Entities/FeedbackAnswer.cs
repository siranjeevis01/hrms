using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Performance.Domain.Entities;

public class FeedbackAnswer : BaseEntity
{
    public Guid Feedback360Id { get; private set; }
    public string Question { get; private set; } = string.Empty;
    public decimal? Rating { get; private set; }
    public string? Comments { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private FeedbackAnswer() { }

    public static FeedbackAnswer Create(
        Guid feedback360Id,
        string question,
        decimal? rating,
        string? comments,
        string tenantId)
    {
        return new FeedbackAnswer
        {
            Id = Guid.NewGuid(),
            Feedback360Id = feedback360Id,
            Question = question,
            Rating = rating,
            Comments = comments,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void UpdateAnswer(decimal? rating, string? comments)
    {
        Rating = rating;
        Comments = comments ?? Comments;
        UpdatedAt = DateTime.UtcNow;
    }
}
