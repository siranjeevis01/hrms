using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Organization.Domain.Entities;

public class Shift : AggregateRoot
{
    public Guid CompanyId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public TimeSpan BreakDuration { get; private set; }
    public int GraceMinutes { get; private set; }
    public bool IsFlexible { get; private set; }
    public int MaxShifts { get; private set; }
    public bool IsActive { get; private set; }
    public Guid TenantId { get; private set; }

    private Shift() { }

    private Shift(
        Guid id,
        Guid companyId,
        string name,
        string code,
        TimeOnly startTime,
        TimeOnly endTime,
        Guid tenantId)
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
        Code = code;
        StartTime = startTime;
        EndTime = endTime;
        TenantId = tenantId;
        IsActive = true;
        BreakDuration = TimeSpan.FromMinutes(30);
        GraceMinutes = 15;
        MaxShifts = 1;
        CreatedAt = DateTime.UtcNow;
    }

    public static Shift Create(
        Guid companyId,
        string name,
        string code,
        TimeOnly startTime,
        TimeOnly endTime,
        Guid tenantId,
        TimeSpan? breakDuration = null,
        int graceMinutes = 15,
        bool isFlexible = false,
        int maxShifts = 1)
    {
        if (companyId == Guid.Empty)
            throw new ArgumentException("Company ID is required.", nameof(companyId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Shift name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Shift code is required.", nameof(code));

        if (startTime >= endTime)
            throw new ArgumentException("Start time must be before end time.");

        if (graceMinutes < 0)
            throw new ArgumentException("Grace minutes cannot be negative.", nameof(graceMinutes));

        if (maxShifts < 1)
            throw new ArgumentException("Max shifts must be at least 1.", nameof(maxShifts));

        return new Shift(Guid.NewGuid(), companyId, name, code, startTime, endTime, tenantId)
        {
            BreakDuration = breakDuration ?? TimeSpan.FromMinutes(30),
            GraceMinutes = graceMinutes,
            IsFlexible = isFlexible,
            MaxShifts = maxShifts
        };
    }

    public void UpdateDetails(
        string? name = null,
        string? code = null,
        TimeOnly? startTime = null,
        TimeOnly? endTime = null,
        TimeSpan? breakDuration = null,
        int? graceMinutes = null,
        bool? isFlexible = null,
        int? maxShifts = null)
    {
        if (!string.IsNullOrWhiteSpace(name)) Name = name;
        if (!string.IsNullOrWhiteSpace(code)) Code = code;
        StartTime = startTime ?? StartTime;
        EndTime = endTime ?? EndTime;
        BreakDuration = breakDuration ?? BreakDuration;
        GraceMinutes = graceMinutes ?? GraceMinutes;
        IsFlexible = isFlexible ?? IsFlexible;
        MaxShifts = maxShifts ?? MaxShifts;

        if (StartTime >= EndTime)
            throw new ArgumentException("Start time must be before end time.");

        if (GraceMinutes < 0)
            throw new ArgumentException("Grace minutes cannot be negative.");

        if (MaxShifts < 1)
            throw new ArgumentException("Max shifts must be at least 1.");

        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
