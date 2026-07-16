using HRMS.Services.Report.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Report.Domain.Entities;

public class ScheduledReport : AggregateRoot
{
    public Guid TemplateId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string CronExpression { get; private set; } = string.Empty;
    public string? Recipients { get; private set; }
    public string? Parameters { get; private set; }
    public ReportFormat Format { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? LastRunAt { get; private set; }
    public DateTime? NextRunAt { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private ScheduledReport() { }

    public static ScheduledReport Create(
        Guid templateId,
        string name,
        string cronExpression,
        string? recipients,
        string? parameters,
        ReportFormat format,
        string tenantId)
    {
        return new ScheduledReport
        {
            Id = Guid.NewGuid(),
            TemplateId = templateId,
            Name = name,
            CronExpression = cronExpression,
            Recipients = recipients,
            Parameters = parameters,
            Format = format,
            IsActive = true,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? name,
        string? cronExpression,
        string? recipients,
        string? parameters,
        ReportFormat? format,
        bool? isActive)
    {
        Name = name ?? Name;
        CronExpression = cronExpression ?? CronExpression;
        Recipients = recipients ?? Recipients;
        Parameters = parameters ?? Parameters;
        if (format.HasValue) Format = format.Value;
        if (isActive.HasValue) IsActive = isActive.Value;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RecordRun(DateTime nextRunAt)
    {
        LastRunAt = DateTime.UtcNow;
        NextRunAt = nextRunAt;
        UpdatedAt = DateTime.UtcNow;
    }
}
