using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.CreateJobPosting;

public class CreateJobPostingCommandHandler : IRequestHandler<CreateJobPostingCommand, Guid>
{
    private readonly IRecruitmentDbContext _context;

    public CreateJobPostingCommandHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateJobPostingCommand request, CancellationToken cancellationToken)
    {
        var jobPosting = Domain.Entities.JobPosting.Create(
            request.Title,
            request.Description,
            request.DepartmentId,
            request.DesignationId,
            request.BranchId,
            request.EmploymentType,
            request.MinExperience,
            request.MaxExperience,
            request.MinSalary,
            request.MaxSalary,
            request.Currency,
            request.Skills,
            request.Requirements,
            request.Responsibilities,
            request.Benefits,
            request.HeadCount,
            request.HiringManagerId,
            request.RecruiterId,
            request.IsUrgent,
            request.ApplicationDeadline,
            request.TenantId);

        _context.JobPostings.Add(jobPosting);
        await _context.SaveChangesAsync(cancellationToken);

        return jobPosting.Id;
    }
}
