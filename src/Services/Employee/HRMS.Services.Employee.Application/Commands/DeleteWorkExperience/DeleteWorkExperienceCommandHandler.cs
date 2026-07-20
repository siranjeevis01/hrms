using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.DeleteWorkExperience;

public class DeleteWorkExperienceCommandHandler : IRequestHandler<DeleteWorkExperienceCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public DeleteWorkExperienceCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteWorkExperienceCommand request, CancellationToken cancellationToken)
    {
        var experience = await _context.WorkExperiences
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Work experience with ID {request.Id} not found.");

        _context.WorkExperiences.Remove(experience);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
