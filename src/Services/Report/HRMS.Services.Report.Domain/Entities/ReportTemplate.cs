using HRMS.Services.Report.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Report.Domain.Entities;

public class ReportTemplate : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public ReportCategory Category { get; private set; }
    public ReportType ReportType { get; private set; }
    public string DataSource { get; private set; } = string.Empty;
    public string? QueryDefinition { get; private set; }
    public string? Parameters { get; private set; }
    public ReportFormat Format { get; private set; }
    public ReportAccessLevel AccessLevel { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private readonly List<ReportInstance> _instances = new();
    public IReadOnlyCollection<ReportInstance> Instances => _instances.AsReadOnly();

    private readonly List<ReportAccess> _accessPermissions = new();
    public IReadOnlyCollection<ReportAccess> AccessPermissions => _accessPermissions.AsReadOnly();

    private ReportTemplate() { }

    public static ReportTemplate Create(
        string name,
        string? description,
        ReportCategory category,
        ReportType reportType,
        string dataSource,
        string? queryDefinition,
        string? parameters,
        ReportFormat format,
        ReportAccessLevel accessLevel,
        string tenantId)
    {
        return new ReportTemplate
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Category = category,
            ReportType = reportType,
            DataSource = dataSource,
            QueryDefinition = queryDefinition,
            Parameters = parameters,
            Format = format,
            AccessLevel = accessLevel,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? name,
        string? description,
        ReportCategory? category,
        string? dataSource,
        string? queryDefinition,
        string? parameters,
        ReportFormat? format,
        ReportAccessLevel? accessLevel)
    {
        Name = name ?? Name;
        Description = description ?? Description;
        if (category.HasValue) Category = category.Value;
        DataSource = dataSource ?? DataSource;
        QueryDefinition = queryDefinition ?? QueryDefinition;
        Parameters = parameters ?? Parameters;
        if (format.HasValue) Format = format.Value;
        if (accessLevel.HasValue) AccessLevel = accessLevel.Value;
        UpdatedAt = DateTime.UtcNow;
    }
}
