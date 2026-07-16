using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetSprint;

public class GetSprintQueryHandler : IRequestHandler<GetSprintQuery, SprintDto?>
{
    private readonly IProjectTaskDbContext _context;
    private readonly IMapper _mapper;

    public GetSprintQueryHandler(IProjectTaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SprintDto?> Handle(GetSprintQuery request, CancellationToken cancellationToken)
    {
        var sprint = await _context.Sprints
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (sprint == null) return null;

        return _mapper.Map<SprintDto>(sprint);
    }
}
