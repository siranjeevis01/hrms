using HRMS.Services.Report.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Report.Domain.Entities;

public class ReportInstance : BaseEntity
{
    public Guid TemplateId { get; private set; }
    public Guid GeneratedBy { get; private set; }
    public DateTime GeneratedAt { get; private set; }
    public string? Parameters { get; private set; }
    public string? FileUrl { get; private set; }
    public long? FileSize { get; private set; }
    public new ReportStatus Status { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private ReportInstance() { }

    public static ReportInstance Create(
        Guid templateId,
        Guid generatedBy,
        string? parameters,
        string tenantId)
    {
        return new ReportInstance
        {
            Id = Guid.NewGuid(),
            TemplateId = templateId,
            GeneratedBy = generatedBy,
            GeneratedAt = DateTime.UtcNow,
            Parameters = parameters,
            Status = ReportStatus.Generating,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void MarkCompleted(string fileUrl, long fileSize)
    {
        FileUrl = fileUrl;
        FileSize = fileSize;
        Status = ReportStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkFailed()
    {
        Status = ReportStatus.Failed;
        UpdatedAt = DateTime.UtcNow;
    }
}
