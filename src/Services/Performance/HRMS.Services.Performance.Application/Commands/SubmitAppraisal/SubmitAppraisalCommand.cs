using MediatR;

namespace HRMS.Services.Performance.Application.Commands.SubmitAppraisal;

public class SubmitAppraisalCommand : IRequest
{
    public Guid AppraisalId { get; set; }
}
