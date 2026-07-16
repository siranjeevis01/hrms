using MediatR;

namespace HRMS.Services.Performance.Application.Commands.ApproveAppraisal;

public class ApproveAppraisalCommand : IRequest
{
    public Guid AppraisalId { get; set; }
    public Guid ApprovedBy { get; set; }
    public decimal? FinalRating { get; set; }
    public decimal? HikePercentage { get; set; }
    public bool PromotionRecommended { get; set; }
    public decimal? Bonus { get; set; }
}
