using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetMyTasks;

public class GetMyTasksQueryHandler : IRequestHandler<GetMyTasksQuery, List<TaskItemDto>>
{
    private readonly IProjectTaskDbContext _context;
    private readonly IMapper _mapper;

    public GetMyTasksQueryHandler(IProjectTaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TaskItemDto>> Handle(GetMyTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _context.TaskItems
            .Where(t => t.AssignedTo == request.EmployeeId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<TaskItemDto>>(tasks);
    }
}
