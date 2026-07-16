using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Performance.Application.Events;

public class ReviewSubmittedEvent : DomainEvent
{
    public Guid ReviewId { get; }
    public Guid EmployeeId { get; }
    public Guid ReviewerId { get; }

    public ReviewSubmittedEvent(Guid reviewId, Guid employeeId, Guid reviewerId) : base("ReviewSubmitted")
    {
        ReviewId = reviewId;
        EmployeeId = employeeId;
        ReviewerId = reviewerId;
    }
}
