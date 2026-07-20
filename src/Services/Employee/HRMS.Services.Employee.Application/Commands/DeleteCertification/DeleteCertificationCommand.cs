using MediatR;

namespace HRMS.Services.Employee.Application.Commands.DeleteCertification;

public class DeleteCertificationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
