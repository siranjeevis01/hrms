using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Performance.Application.Events;

public class AppraisalApprovedEvent : DomainEvent
{
    public Guid AppraisalId { get; }
    public Guid EmployeeId { get; }
    public decimal? FinalRating { get; }
    public decimal? HikePercentage { get; }

    public AppraisalApprovedEvent(Guid appraisalId, Guid employeeId, decimal? finalRating, decimal? hikePercentage) : base("AppraisalApproved")
    {
        AppraisalId = appraisalId;
        EmployeeId = employeeId;
        FinalRating = finalRating;
        HikePercentage = hikePercentage;
    }
}
