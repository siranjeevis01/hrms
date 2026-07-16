using HRMS.Services.ProjectTask.Application.Interfaces;
using HRMS.Services.ProjectTask.Domain.Entities;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.AddProjectMember;

public class AddProjectMemberCommandHandler : IRequestHandler<AddProjectMemberCommand, Guid>
{
    private readonly IProjectTaskDbContext _context;

    public AddProjectMemberCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddProjectMemberCommand request, CancellationToken cancellationToken)
    {
        var member = ProjectMember.Create(
            request.ProjectId,
            request.EmployeeId,
            request.Role,
            request.AllocationPercentage,
            request.TenantId);

        _context.ProjectMembers.Add(member);
        await _context.SaveChangesAsync(cancellationToken);

        return member.Id;
    }
}
