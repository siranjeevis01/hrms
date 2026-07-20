using MediatR;

namespace HRMS.Services.Employee.Application.Commands.DeleteDependent;

public class DeleteDependentCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
