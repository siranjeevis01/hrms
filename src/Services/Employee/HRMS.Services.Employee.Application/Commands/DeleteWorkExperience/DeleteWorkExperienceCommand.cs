using MediatR;

namespace HRMS.Services.Employee.Application.Commands.DeleteWorkExperience;

public class DeleteWorkExperienceCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
