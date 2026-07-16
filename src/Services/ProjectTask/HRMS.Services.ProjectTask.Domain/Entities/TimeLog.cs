using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.ProjectTask.Domain.Entities;

public class TimeLog : BaseEntity
{
    public Guid? TaskItemId { get; private set; }
    public Guid? StoryId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public decimal Hours { get; private set; }
    public DateTime Date { get; private set; }
    public string? Description { get; private set; }

    private TimeLog() { }

    public static TimeLog Create(
        Guid? taskItemId,
        Guid? storyId,
        Guid employeeId,
        decimal hours,
        DateTime date,
        string? description,
        Guid tenantId)
    {
        return new TimeLog
        {
            Id = Guid.NewGuid(),
            TaskItemId = taskItemId,
            StoryId = storyId,
            EmployeeId = employeeId,
            Hours = hours,
            Date = date,
            Description = description,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(decimal hours, DateTime date, string? description)
    {
        Hours = hours;
        Date = date;
        Description = description ?? Description;
        UpdatedAt = DateTime.UtcNow;
    }
}
