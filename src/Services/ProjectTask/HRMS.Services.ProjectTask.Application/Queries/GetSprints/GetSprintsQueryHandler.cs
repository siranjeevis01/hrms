using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetSprints;

public class GetSprintsQueryHandler : IRequestHandler<GetSprintsQuery, List<SprintDto>>
{
    private readonly IProjectTaskDbContext _context;
    private readonly IMapper _mapper;

    public GetSprintsQueryHandler(IProjectTaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SprintDto>> Handle(GetSprintsQuery request, CancellationToken cancellationToken)
    {
        var sprints = await _context.Sprints
            .Where(s => s.ProjectId == request.ProjectId)
            .OrderByDescending(s => s.StartDate)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<SprintDto>>(sprints);
    }
}
