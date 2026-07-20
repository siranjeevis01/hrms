using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Helpdesk.Domain.Entities;

public class TicketComment : BaseEntity
{
    public Guid TicketId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public bool IsInternal { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private TicketComment() { }

    public static TicketComment Create(
        Guid ticketId,
        Guid employeeId,
        string content,
        bool isInternal,
        string tenantId)
    {
        return new TicketComment
        {
            Id = Guid.NewGuid(),
            TicketId = ticketId,
            EmployeeId = employeeId,
            Content = content,
            IsInternal = isInternal,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? content, bool? isInternal)
    {
        Content = content ?? Content;
        IsInternal = isInternal ?? IsInternal;
        UpdatedAt = DateTime.UtcNow;
    }
}
