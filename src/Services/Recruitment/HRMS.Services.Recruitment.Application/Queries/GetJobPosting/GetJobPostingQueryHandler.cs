using AutoMapper;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Queries.GetJobPosting;

public class GetJobPostingQueryHandler : IRequestHandler<GetJobPostingQuery, JobPostingDto?>
{
    private readonly IRecruitmentDbContext _context;
    private readonly IMapper _mapper;

    public GetJobPostingQueryHandler(IRecruitmentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<JobPostingDto?> Handle(GetJobPostingQuery request, CancellationToken cancellationToken)
    {
        var jobPosting = await _context.JobPostings
            .FirstOrDefaultAsync(j => j.Id == request.Id, cancellationToken);

        if (jobPosting == null)
            return null;

        return _mapper.Map<JobPostingDto>(jobPosting);
    }
}
