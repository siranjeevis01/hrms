using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Performance.Application.Events;

public class GoalCompletedEvent : DomainEvent
{
    public Guid GoalId { get; }
    public Guid EmployeeId { get; }
    public string Title { get; }

    public GoalCompletedEvent(Guid goalId, Guid employeeId, string title) : base("GoalCompleted")
    {
        GoalId = goalId;
        EmployeeId = employeeId;
        Title = title;
    }
}
