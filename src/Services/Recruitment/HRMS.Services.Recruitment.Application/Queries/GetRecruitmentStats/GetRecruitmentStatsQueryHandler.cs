using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Interfaces;
using HRMS.Services.Recruitment.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Queries.GetRecruitmentStats;

public class GetRecruitmentStatsQueryHandler : IRequestHandler<GetRecruitmentStatsQuery, RecruitmentStatsDto>
{
    private readonly IRecruitmentDbContext _context;

    public GetRecruitmentStatsQueryHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<RecruitmentStatsDto> Handle(GetRecruitmentStatsQuery request, CancellationToken cancellationToken)
    {
        var jobPostingsQuery = _context.JobPostings.AsQueryable();
        var applicationsQuery = _context.JobApplications.AsQueryable();
        var interviewsQuery = _context.Interviews.AsQueryable();
        var offersQuery = _context.OfferLetters.AsQueryable();

        if (request.TenantId.HasValue)
        {
            jobPostingsQuery = jobPostingsQuery.Where(j => j.TenantId == request.TenantId.Value);
            applicationsQuery = applicationsQuery.Where(a => a.TenantId == request.TenantId.Value);
            interviewsQuery = interviewsQuery.Where(i => i.TenantId == request.TenantId.Value);
            offersQuery = offersQuery.Where(o => o.TenantId == request.TenantId.Value);
        }

        var totalJobs = await jobPostingsQuery.CountAsync(cancellationToken);
        var activeJobs = await jobPostingsQuery.CountAsync(j => j.Status == JobStatus.Published, cancellationToken);
        var totalApplications = await applicationsQuery.CountAsync(cancellationToken);
        var pendingApplications = await applicationsQuery.CountAsync(a => a.Status == ApplicationStatus.Applied, cancellationToken);
        var totalInterviews = await interviewsQuery.CountAsync(cancellationToken);
        var scheduledInterviews = await interviewsQuery.CountAsync(i => i.Status == InterviewStatus.Scheduled, cancellationToken);
        var totalOffers = await offersQuery.CountAsync(cancellationToken);
        var totalHires = await offersQuery.CountAsync(o => o.Status == OfferStatus.Accepted, cancellationToken);

        return new RecruitmentStatsDto
        {
            TotalJobs = totalJobs,
            ActiveJobs = activeJobs,
            TotalApplications = totalApplications,
            PendingApplications = pendingApplications,
            TotalInterviews = totalInterviews,
            ScheduledInterviews = scheduledInterviews,
            TotalOffers = totalOffers,
            TotalHires = totalHires
        };
    }
}
