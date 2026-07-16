using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetProjectMembers;

public class GetProjectMembersQueryHandler : IRequestHandler<GetProjectMembersQuery, List<ProjectMemberDto>>
{
    private readonly IProjectTaskDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectMembersQueryHandler(IProjectTaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProjectMemberDto>> Handle(GetProjectMembersQuery request, CancellationToken cancellationToken)
    {
        var members = await _context.ProjectMembers
            .Where(m => m.ProjectId == request.ProjectId)
            .OrderBy(m => m.Role)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ProjectMemberDto>>(members);
    }
}
