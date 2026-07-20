using MediatR;

namespace HRMS.Services.Employee.Application.Commands.DeleteSkill;

public class DeleteSkillCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
