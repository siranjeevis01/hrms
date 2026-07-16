using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.RemoveProjectMember;

public class RemoveProjectMemberCommandHandler : IRequestHandler<RemoveProjectMemberCommand>
{
    private readonly IProjectTaskDbContext _context;

    public RemoveProjectMemberCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveProjectMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _context.ProjectMembers
            .FirstOrDefaultAsync(m => m.Id == request.MemberId && m.ProjectId == request.ProjectId, cancellationToken)
            ?? throw new InvalidOperationException($"Member with ID {request.MemberId} not found in project.");

        _context.ProjectMembers.Remove(member);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
