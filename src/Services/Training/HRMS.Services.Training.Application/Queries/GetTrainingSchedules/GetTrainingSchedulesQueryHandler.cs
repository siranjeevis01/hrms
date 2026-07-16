using AutoMapper;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetTrainingSchedules;

public class GetTrainingSchedulesQueryHandler : IRequestHandler<GetTrainingSchedulesQuery, List<TrainingScheduleDto>>
{
    private readonly ITrainingDbContext _context;
    private readonly IMapper _mapper;

    public GetTrainingSchedulesQueryHandler(ITrainingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TrainingScheduleDto>> Handle(GetTrainingSchedulesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TrainingSchedules
            .Where(ts => !ts.IsDeleted);

        if (request.CourseId.HasValue)
            query = query.Where(ts => ts.CourseId == request.CourseId.Value);

        if (!string.IsNullOrWhiteSpace(request.Status))
            query = query.Where(ts => ts.Status.ToString() == request.Status);

        var schedules = await query
            .OrderByDescending(ts => ts.StartDate)
            .ToListAsync(cancellationToken);

        var dtos = new List<TrainingScheduleDto>();
        foreach (var schedule in schedules)
        {
            var dto = _mapper.Map<TrainingScheduleDto>(schedule);
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == schedule.CourseId, cancellationToken);
            dto.CourseTitle = course?.Title;
            dtos.Add(dto);
        }
        return dtos;
    }
}
