using HRMS.Services.Helpdesk.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Helpdesk.Domain.Entities;

public class HelpdeskTicket : AggregateRoot
{
    public Guid EmployeeId { get; private set; }
    public string Subject { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public TicketCategoryType Category { get; private set; }
    public TicketPriority Priority { get; private set; }
    public new TicketStatus Status { get; private set; }
    public Guid? AssignedTo { get; private set; }
    public Guid? DepartmentId { get; private set; }
    public string? ResolutionNotes { get; private set; }
    public DateTime? ResolvedAt { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private readonly List<TicketComment> _comments = new();
    public IReadOnlyCollection<TicketComment> Comments => _comments.AsReadOnly();

    private readonly List<TicketAttachment> _attachments = new();
    public IReadOnlyCollection<TicketAttachment> Attachments => _attachments.AsReadOnly();

    private HelpdeskTicket() { }

    public static HelpdeskTicket Create(
        Guid employeeId,
        string subject,
        string description,
        TicketCategoryType category,
        TicketPriority priority,
        Guid? departmentId,
        string tenantId)
    {
        return new HelpdeskTicket
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            Subject = subject,
            Description = description,
            Category = category,
            Priority = priority,
            Status = TicketStatus.Open,
            DepartmentId = departmentId,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Assign(Guid assignedTo)
    {
        AssignedTo = assignedTo;
        if (Status == TicketStatus.Open)
            Status = TicketStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangePriority(TicketPriority newPriority)
    {
        Priority = newPriority;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Resolve(string? resolutionNotes)
    {
        Status = TicketStatus.Resolved;
        ResolutionNotes = resolutionNotes;
        ResolvedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Close()
    {
        if (Status != TicketStatus.Resolved)
            throw new InvalidOperationException("Only resolved tickets can be closed.");

        Status = TicketStatus.Closed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reopen()
    {
        if (Status != TicketStatus.Resolved && Status != TicketStatus.Closed)
            throw new InvalidOperationException("Only resolved or closed tickets can be reopened.");

        Status = TicketStatus.Open;
        ResolutionNotes = null;
        ResolvedAt = null;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(
        string? subject,
        string? description,
        TicketCategoryType? category,
        TicketPriority? priority,
        Guid? departmentId)
    {
        if (Status == TicketStatus.Closed)
            throw new InvalidOperationException("Closed tickets cannot be updated.");

        Subject = subject ?? Subject;
        Description = description ?? Description;
        Category = category ?? Category;
        Priority = priority ?? Priority;
        DepartmentId = departmentId ?? DepartmentId;
        UpdatedAt = DateTime.UtcNow;
    }
}
