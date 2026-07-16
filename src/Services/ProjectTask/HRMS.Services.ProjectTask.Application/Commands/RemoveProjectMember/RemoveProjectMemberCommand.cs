using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.RemoveProjectMember;

public class RemoveProjectMemberCommand : IRequest
{
    public Guid ProjectId { get; set; }
    public Guid MemberId { get; set; }
}
