using MediatR;

namespace HRMS.Services.Employee.Application.Commands.DeleteEducation;

public class DeleteEducationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
