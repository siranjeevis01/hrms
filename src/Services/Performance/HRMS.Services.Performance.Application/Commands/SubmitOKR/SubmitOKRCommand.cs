using MediatR;

namespace HRMS.Services.Performance.Application.Commands.SubmitOKR;

public class SubmitOKRCommand : IRequest
{
    public Guid OKRId { get; set; }
}
