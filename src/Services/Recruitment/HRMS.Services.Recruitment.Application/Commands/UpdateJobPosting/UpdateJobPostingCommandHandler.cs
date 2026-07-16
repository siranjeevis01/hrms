using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Commands.UpdateJobPosting;

public class UpdateJobPostingCommandHandler : IRequestHandler<UpdateJobPostingCommand, Unit>
{
    private readonly IRecruitmentDbContext _context;

    public UpdateJobPostingCommandHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateJobPostingCommand request, CancellationToken cancellationToken)
    {
        var jobPosting = await _context.JobPostings
            .FirstOrDefaultAsync(j => j.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Job posting with ID {request.Id} not found.");

        jobPosting.Update(
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
            request.IsUrgent,
            request.ApplicationDeadline);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
