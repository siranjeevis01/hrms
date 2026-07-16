using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetTask;

public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, TaskItemDto?>
{
    private readonly IProjectTaskDbContext _context;
    private readonly IMapper _mapper;

    public GetTaskQueryHandler(IProjectTaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TaskItemDto?> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var task = await _context.TaskItems
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (task == null) return null;

        return _mapper.Map<TaskItemDto>(task);
    }
}
