using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Expense.Domain.Entities;

public class ExpenseCategoryEntity : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Guid? DefaultPolicyId { get; private set; }
    public bool IsActive { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private ExpenseCategoryEntity() { }

    public static ExpenseCategoryEntity Create(
        string name,
        string code,
        string? description,
        Guid? defaultPolicyId,
        string tenantId)
    {
        return new ExpenseCategoryEntity
        {
            Id = Guid.NewGuid(),
            Name = name,
            Code = code,
            Description = description,
            DefaultPolicyId = defaultPolicyId,
            IsActive = true,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? name,
        string? code,
        string? description,
        Guid? defaultPolicyId,
        bool? isActive)
    {
        Name = name ?? Name;
        Code = code ?? Code;
        Description = description ?? Description;
        DefaultPolicyId = defaultPolicyId ?? DefaultPolicyId;
        IsActive = isActive ?? IsActive;
        UpdatedAt = DateTime.UtcNow;
    }
}
