using MediatR;

namespace HRMS.Services.Performance.Application.Commands.RejectAppraisal;

public class RejectAppraisalCommand : IRequest
{
    public Guid AppraisalId { get; set; }
}
