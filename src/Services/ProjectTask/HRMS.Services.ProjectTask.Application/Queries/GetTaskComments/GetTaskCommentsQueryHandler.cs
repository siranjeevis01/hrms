using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetTaskComments;

public class GetTaskCommentsQueryHandler : IRequestHandler<GetTaskCommentsQuery, List<CommentDto>>
{
    private readonly IProjectTaskDbContext _context;
    private readonly IMapper _mapper;

    public GetTaskCommentsQueryHandler(IProjectTaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CommentDto>> Handle(GetTaskCommentsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Comments.AsQueryable();

        if (request.TaskItemId.HasValue)
            query = query.Where(c => c.TaskItemId == request.TaskItemId.Value);
        else if (request.StoryId.HasValue)
            query = query.Where(c => c.StoryId == request.StoryId.Value);
        else if (request.BugId.HasValue)
            query = query.Where(c => c.BugId == request.BugId.Value);
        else
            return new List<CommentDto>();

        var comments = await query
            .OrderBy(c => c.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<CommentDto>>(comments);
    }
}
