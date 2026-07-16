using HRMS.Services.Employee.Application.Interfaces;
using HRMS.Services.Employee.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.AddSkill;

public class AddSkillCommandHandler : IRequestHandler<AddSkillCommand, Guid>
{
    private readonly IEmployeeDbContext _context;

    public AddSkillCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddSkillCommand request, CancellationToken cancellationToken)
    {
        var employeeExists = await _context.Employees
            .AnyAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (!employeeExists)
            throw new InvalidOperationException($"Employee with ID {request.EmployeeId} not found.");

        var skill = Skill.Create(
            request.EmployeeId, request.Name, request.Category,
            request.Proficiency, request.YearsOfExperience,
            request.LastUsedDate, request.IsEndorsed, request.EndorsedBy);

        _context.Skills.Add(skill);
        await _context.SaveChangesAsync(cancellationToken);

        return skill.Id;
    }
}
