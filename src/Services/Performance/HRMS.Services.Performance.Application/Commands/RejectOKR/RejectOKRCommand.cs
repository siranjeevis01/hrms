using MediatR;

namespace HRMS.Services.Performance.Application.Commands.RejectOKR;

public class RejectOKRCommand : IRequest
{
    public Guid OKRId { get; set; }
}
