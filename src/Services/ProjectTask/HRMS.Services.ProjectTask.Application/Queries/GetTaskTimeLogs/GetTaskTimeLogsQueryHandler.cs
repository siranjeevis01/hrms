using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetTaskTimeLogs;

public class GetTaskTimeLogsQueryHandler : IRequestHandler<GetTaskTimeLogsQuery, List<TimeLogDto>>
{
    private readonly IProjectTaskDbContext _context;
    private readonly IMapper _mapper;

    public GetTaskTimeLogsQueryHandler(IProjectTaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TimeLogDto>> Handle(GetTaskTimeLogsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TimeLogs.AsQueryable();

        if (request.TaskItemId.HasValue)
            query = query.Where(t => t.TaskItemId == request.TaskItemId.Value);
        else if (request.StoryId.HasValue)
            query = query.Where(t => t.StoryId == request.StoryId.Value);
        else
            return new List<TimeLogDto>();

        var timeLogs = await query
            .OrderByDescending(t => t.Date)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<TimeLogDto>>(timeLogs);
    }
}
