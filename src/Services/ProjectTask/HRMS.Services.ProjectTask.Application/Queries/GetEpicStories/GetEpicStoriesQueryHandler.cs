using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetEpicStories;

public class GetEpicStoriesQueryHandler : IRequestHandler<GetEpicStoriesQuery, List<StoryDto>>
{
    private readonly IProjectTaskDbContext _context;
    private readonly IMapper _mapper;

    public GetEpicStoriesQueryHandler(IProjectTaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<StoryDto>> Handle(GetEpicStoriesQuery request, CancellationToken cancellationToken)
    {
        var stories = await _context.Stories
            .Where(s => s.EpicId == request.EpicId)
            .OrderBy(s => s.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<StoryDto>>(stories);
    }
}
