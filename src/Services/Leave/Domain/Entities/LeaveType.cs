using HRMS.Services.Leave.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Domain.Entities;

public enum GenderRestriction
{
    All,
    Male,
    Female
}

public class LeaveType : AggregateRoot
{
    private LeaveType() { }

    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? Color { get; private set; }
    public string? Icon { get; private set; }
    public bool IsPaid { get; private set; }
    public bool IsUnlimited { get; private set; }
    public int DefaultBalanceDays { get; private set; }
    public int MaxBalanceDays { get; private set; }
    public int MaxCarryForwardDays { get; private set; }
    public int MaxEncashmentDays { get; private set; }
    public int? CarryForwardExpiryMonths { get; private set; }
    public bool AllowEncashment { get; private set; }
    public bool AllowCarryForward { get; private set; }
    public bool AllowHalfDay { get; private set; }
    public int MinDaysPerRequest { get; private set; }
    public int MaxDaysPerRequest { get; private set; }
    public int? MaxConsecutiveDays { get; private set; }
    public bool RequireDocumentation { get; private set; }
    public int? DocumentationDaysThreshold { get; private set; }
    public GenderRestriction Gender { get; private set; }
    public int? ApplicableAfterDays { get; private set; }
    public AccrualType AccrualType { get; private set; }
    public decimal AccrualRate { get; private set; }
    public bool IsActive { get; private set; }

    public static LeaveType Create(
        Guid id,
        string name,
        string code,
        string? description,
        string? color,
        string? icon,
        bool isPaid,
        bool isUnlimited,
        int defaultBalanceDays,
        int maxBalanceDays,
        int maxCarryForwardDays,
        int maxEncashmentDays,
        int? carryForwardExpiryMonths,
        bool allowEncashment,
        bool allowCarryForward,
        bool allowHalfDay,
        int minDaysPerRequest,
        int maxDaysPerRequest,
        int? maxConsecutiveDays,
        bool requireDocumentation,
        int? documentationDaysThreshold,
        GenderRestriction gender,
        int? applicableAfterDays,
        AccrualType accrualType,
        decimal accrualRate,
        Guid tenantId)
    {
        return new LeaveType
        {
            Id = id,
            Name = name,
            Code = code,
            Description = description,
            Color = color,
            Icon = icon,
            IsPaid = isPaid,
            IsUnlimited = isUnlimited,
            DefaultBalanceDays = defaultBalanceDays,
            MaxBalanceDays = maxBalanceDays,
            MaxCarryForwardDays = maxCarryForwardDays,
            MaxEncashmentDays = maxEncashmentDays,
            CarryForwardExpiryMonths = carryForwardExpiryMonths,
            AllowEncashment = allowEncashment,
            AllowCarryForward = allowCarryForward,
            AllowHalfDay = allowHalfDay,
            MinDaysPerRequest = minDaysPerRequest,
            MaxDaysPerRequest = maxDaysPerRequest,
            MaxConsecutiveDays = maxConsecutiveDays,
            RequireDocumentation = requireDocumentation,
            DocumentationDaysThreshold = documentationDaysThreshold,
            Gender = gender,
            ApplicableAfterDays = applicableAfterDays,
            AccrualType = accrualType,
            AccrualRate = accrualRate,
            IsActive = true,
            TenantId = tenantId
        };
    }

    public void Update(string name, string? description, string? color, string? icon,
        bool isPaid, int maxBalanceDays, int maxCarryForwardDays, int maxEncashmentDays,
        int? carryForwardExpiryMonths, bool allowEncashment, bool allowCarryForward,
        bool allowHalfDay, int minDaysPerRequest, int maxDaysPerRequest,
        int? maxConsecutiveDays, bool requireDocumentation, int? documentationDaysThreshold,
        GenderRestriction gender, int? applicableAfterDays, AccrualType accrualType,
        decimal accrualRate, bool isActive)
    {
        Name = name;
        Description = description;
        Color = color;
        Icon = icon;
        IsPaid = isPaid;
        MaxBalanceDays = maxBalanceDays;
        MaxCarryForwardDays = maxCarryForwardDays;
        MaxEncashmentDays = maxEncashmentDays;
        CarryForwardExpiryMonths = carryForwardExpiryMonths;
        AllowEncashment = allowEncashment;
        AllowCarryForward = allowCarryForward;
        AllowHalfDay = allowHalfDay;
        MinDaysPerRequest = minDaysPerRequest;
        MaxDaysPerRequest = maxDaysPerRequest;
        MaxConsecutiveDays = maxConsecutiveDays;
        RequireDocumentation = requireDocumentation;
        DocumentationDaysThreshold = documentationDaysThreshold;
        Gender = gender;
        ApplicableAfterDays = applicableAfterDays;
        AccrualType = accrualType;
        AccrualRate = accrualRate;
        IsActive = isActive;
        UpdatedAt = DateTime.UtcNow;
    }
}
