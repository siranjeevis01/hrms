using HRMS.Services.ProjectTask.Application.DTOs;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Queries.GetProjectMembers;

public class GetProjectMembersQuery : IRequest<List<ProjectMemberDto>>
{
    public Guid ProjectId { get; set; }
}
