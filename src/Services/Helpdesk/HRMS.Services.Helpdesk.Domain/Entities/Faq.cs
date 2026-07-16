using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Helpdesk.Domain.Entities;

public class Faq : BaseEntity
{
    public string Question { get; private set; } = string.Empty;
    public string Answer { get; private set; } = string.Empty;
    public Guid? CategoryId { get; private set; }
    public int Order { get; private set; }
    public bool IsActive { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private Faq() { }

    public static Faq Create(
        string question,
        string answer,
        Guid? categoryId,
        int order,
        string tenantId)
    {
        return new Faq
        {
            Id = Guid.NewGuid(),
            Question = question,
            Answer = answer,
            CategoryId = categoryId,
            Order = order,
            IsActive = true,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? question,
        string? answer,
        Guid? categoryId,
        int? order,
        bool? isActive)
    {
        Question = question ?? Question;
        Answer = answer ?? Answer;
        CategoryId = categoryId ?? CategoryId;
        Order = order ?? Order;
        IsActive = isActive ?? IsActive;
        UpdatedAt = DateTime.UtcNow;
    }
}
