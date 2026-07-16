using AutoMapper;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Recruitment.Application.Queries.GetInterviews;

public class GetInterviewsQueryHandler : IRequestHandler<GetInterviewsQuery, PagedResult<InterviewDto>>
{
    private readonly IRecruitmentDbContext _context;
    private readonly IMapper _mapper;

    public GetInterviewsQueryHandler(IRecruitmentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<InterviewDto>> Handle(GetInterviewsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Interviews
            .Include(i => i.Candidate)
            .AsQueryable();

        if (request.CandidateId.HasValue)
            query = query.Where(i => i.CandidateId == request.CandidateId.Value);

        if (request.TenantId.HasValue)
            query = query.Where(i => i.TenantId == request.TenantId.Value);

        if (request.JobPostingId.HasValue)
            query = query.Where(i => i.JobApplication != null && i.JobApplication.JobPostingId == request.JobPostingId.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var interviews = await query
            .OrderBy(i => i.ScheduledAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<InterviewDto>>(interviews);

        return PagedResult<InterviewDto>.Create(items, totalCount, request.PageNumber, request.PageSize);
    }
}
