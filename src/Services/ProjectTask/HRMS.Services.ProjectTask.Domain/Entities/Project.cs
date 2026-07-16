using HRMS.Services.ProjectTask.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.ProjectTask.Domain.Entities;

public class Project : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public Guid DepartmentId { get; private set; }
    public string? ClientName { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public new ProjectStatus Status { get; private set; }
    public ProjectPriority Priority { get; private set; }
    public decimal Budget { get; private set; }
    public decimal ActualCost { get; private set; }
    public string? Currency { get; private set; }
    public Guid? ProjectManagerId { get; private set; }
    public Guid? OwnerId { get; private set; }
    public decimal ProgressPercentage { get; private set; }

    private readonly List<ProjectMember> _members = new();
    public IReadOnlyCollection<ProjectMember> Members => _members.AsReadOnly();

    private readonly List<Epic> _epics = new();
    public IReadOnlyCollection<Epic> Epics => _epics.AsReadOnly();

    private readonly List<Sprint> _sprints = new();
    public IReadOnlyCollection<Sprint> Sprints => _sprints.AsReadOnly();

    private readonly List<Board> _boards = new();
    public IReadOnlyCollection<Board> Boards => _boards.AsReadOnly();

    private Project() { }

    public static Project Create(
        string name,
        string? description,
        string code,
        Guid departmentId,
        string? clientName,
        DateTime startDate,
        DateTime? endDate,
        ProjectPriority priority,
        decimal budget,
        string? currency,
        Guid? ownerId,
        Guid tenantId)
    {
        return new Project
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Code = code,
            DepartmentId = departmentId,
            ClientName = clientName,
            StartDate = startDate,
            EndDate = endDate,
            Status = ProjectStatus.Planning,
            Priority = priority,
            Budget = budget,
            ActualCost = 0,
            Currency = currency ?? "USD",
            OwnerId = ownerId,
            ProgressPercentage = 0,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? name,
        string? description,
        string? clientName,
        DateTime? startDate,
        DateTime? endDate,
        ProjectPriority? priority,
        decimal? budget,
        string? currency)
    {
        Name = name ?? Name;
        Description = description ?? Description;
        ClientName = clientName ?? ClientName;
        StartDate = startDate ?? StartDate;
        EndDate = endDate ?? EndDate;
        Priority = priority ?? Priority;
        Budget = budget ?? Budget;
        Currency = currency ?? Currency;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(ProjectStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateProgress(decimal percentage)
    {
        ProgressPercentage = Math.Clamp(percentage, 0, 100);
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddMember(ProjectMember member)
    {
        _members.Add(member);
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveMember(Guid memberId)
    {
        var member = _members.FirstOrDefault(m => m.Id == memberId);
        if (member != null)
        {
            _members.Remove(member);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void AssignManager(Guid employeeId)
    {
        ProjectManagerId = employeeId;
        UpdatedAt = DateTime.UtcNow;
    }
}
