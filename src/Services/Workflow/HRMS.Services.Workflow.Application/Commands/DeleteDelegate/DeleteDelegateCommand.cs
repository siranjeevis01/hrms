using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.DeleteDelegate;

public class DeleteDelegateCommand : IRequest
{
    public Guid Id { get; set; }
}
