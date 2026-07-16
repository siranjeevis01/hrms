using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetProject;

public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectDto?>
{
    private readonly IProjectTaskDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectQueryHandler(IProjectTaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProjectDto?> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .Include(p => p.Members)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (project == null)
            return null;

        return _mapper.Map<ProjectDto>(project);
    }
}
