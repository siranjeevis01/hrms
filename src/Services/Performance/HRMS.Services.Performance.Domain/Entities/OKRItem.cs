using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Performance.Domain.Entities;

public class OKRItem : BaseEntity
{
    public Guid OKRId { get; private set; }
    public string ObjectiveTitle { get; private set; } = string.Empty;
    public string? ObjectiveDescription { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private readonly List<KeyResult> _keyResults = new();
    public IReadOnlyCollection<KeyResult> KeyResults => _keyResults.AsReadOnly();

    private OKRItem() { }

    public static OKRItem Create(
        Guid okrId,
        string objectiveTitle,
        string? objectiveDescription,
        string tenantId)
    {
        return new OKRItem
        {
            Id = Guid.NewGuid(),
            OKRId = okrId,
            ObjectiveTitle = objectiveTitle,
            ObjectiveDescription = objectiveDescription,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? objectiveTitle, string? objectiveDescription)
    {
        ObjectiveTitle = objectiveTitle ?? ObjectiveTitle;
        ObjectiveDescription = objectiveDescription ?? ObjectiveDescription;
        UpdatedAt = DateTime.UtcNow;
    }
}
