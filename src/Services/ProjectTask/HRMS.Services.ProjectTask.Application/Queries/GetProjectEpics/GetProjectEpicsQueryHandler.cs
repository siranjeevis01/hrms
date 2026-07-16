using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetProjectEpics;

public class GetProjectEpicsQueryHandler : IRequestHandler<GetProjectEpicsQuery, List<EpicDto>>
{
    private readonly IProjectTaskDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectEpicsQueryHandler(IProjectTaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EpicDto>> Handle(GetProjectEpicsQuery request, CancellationToken cancellationToken)
    {
        var epics = await _context.Epics
            .Where(e => e.ProjectId == request.ProjectId)
            .OrderBy(e => e.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<EpicDto>>(epics);
    }
}
