using MediatR;

namespace HRMS.Services.Travel.Application.Commands.CompleteTravelRequest;

public class CompleteTravelRequestCommand : IRequest
{
    public Guid Id { get; set; }
}
