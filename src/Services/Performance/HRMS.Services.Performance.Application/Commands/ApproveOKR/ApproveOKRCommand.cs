using MediatR;

namespace HRMS.Services.Performance.Application.Commands.ApproveOKR;

public class ApproveOKRCommand : IRequest
{
    public Guid OKRId { get; set; }
    public decimal? OverallScore { get; set; }
}
