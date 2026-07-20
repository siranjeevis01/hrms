using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.UpdateSkill;

public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public UpdateSkillCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Skill with ID {request.Id} not found.");

        skill.Update(
            request.Name, request.Category, request.Proficiency,
            request.YearsOfExperience, request.LastUsedDate,
            request.IsEndorsed, request.EndorsedBy);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
