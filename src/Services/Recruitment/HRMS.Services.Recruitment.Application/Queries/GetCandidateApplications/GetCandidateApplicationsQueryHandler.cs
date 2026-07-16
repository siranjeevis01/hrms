using AutoMapper;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Queries.GetCandidateApplications;

public class GetCandidateApplicationsQueryHandler : IRequestHandler<GetCandidateApplicationsQuery, List<JobApplicationDto>>
{
    private readonly IRecruitmentDbContext _context;
    private readonly IMapper _mapper;

    public GetCandidateApplicationsQueryHandler(IRecruitmentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<JobApplicationDto>> Handle(GetCandidateApplicationsQuery request, CancellationToken cancellationToken)
    {
        var applications = await _context.JobApplications
            .Include(a => a.Candidate)
            .Include(a => a.JobPosting)
            .Where(a => a.CandidateId == request.CandidateId)
            .OrderByDescending(a => a.AppliedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<JobApplicationDto>>(applications);
    }
}
