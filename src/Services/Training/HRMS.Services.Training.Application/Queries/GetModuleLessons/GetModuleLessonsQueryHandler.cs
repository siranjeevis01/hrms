using AutoMapper;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetModuleLessons;

public class GetModuleLessonsQueryHandler : IRequestHandler<GetModuleLessonsQuery, List<LessonDto>>
{
    private readonly ITrainingDbContext _context;
    private readonly IMapper _mapper;

    public GetModuleLessonsQueryHandler(ITrainingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<LessonDto>> Handle(GetModuleLessonsQuery request, CancellationToken cancellationToken)
    {
        var lessons = await _context.Lessons
            .Where(l => l.ModuleId == request.ModuleId && !l.IsDeleted)
            .OrderBy(l => l.Order)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<LessonDto>>(lessons);
    }
}
