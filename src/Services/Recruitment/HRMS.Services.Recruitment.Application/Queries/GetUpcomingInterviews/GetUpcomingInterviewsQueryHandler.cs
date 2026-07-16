using AutoMapper;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Interfaces;
using HRMS.Services.Recruitment.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Queries.GetUpcomingInterviews;

public class GetUpcomingInterviewsQueryHandler : IRequestHandler<GetUpcomingInterviewsQuery, List<InterviewDto>>
{
    private readonly IRecruitmentDbContext _context;
    private readonly IMapper _mapper;

    public GetUpcomingInterviewsQueryHandler(IRecruitmentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<InterviewDto>> Handle(GetUpcomingInterviewsQuery request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var nextWeek = now.AddDays(7);

        var query = _context.Interviews
            .Include(i => i.Candidate)
            .Where(i => i.Status == InterviewStatus.Scheduled && i.ScheduledAt >= now && i.ScheduledAt <= nextWeek)
            .AsQueryable();

        if (request.TenantId.HasValue)
            query = query.Where(i => i.TenantId == request.TenantId.Value);

        var interviews = await query
            .OrderBy(i => i.ScheduledAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<InterviewDto>>(interviews);
    }
}
