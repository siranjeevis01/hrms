using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Commands.ShortlistApplication;

public class ShortlistApplicationCommandHandler : IRequestHandler<ShortlistApplicationCommand, Unit>
{
    private readonly IRecruitmentDbContext _context;

    public ShortlistApplicationCommandHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ShortlistApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = await _context.JobApplications
            .FirstOrDefaultAsync(a => a.Id == request.JobApplicationId, cancellationToken)
            ?? throw new InvalidOperationException($"Job application with ID {request.JobApplicationId} not found.");

        application.Shortlist(request.AssignedTo);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
