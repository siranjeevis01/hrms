using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Commands.ScreenApplication;

public class ScreenApplicationCommandHandler : IRequestHandler<ScreenApplicationCommand, Unit>
{
    private readonly IRecruitmentDbContext _context;

    public ScreenApplicationCommandHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ScreenApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = await _context.JobApplications
            .FirstOrDefaultAsync(a => a.Id == request.JobApplicationId, cancellationToken)
            ?? throw new InvalidOperationException($"Job application with ID {request.JobApplicationId} not found.");

        application.SetScreeningScore(request.ScreeningScore, request.Notes);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
