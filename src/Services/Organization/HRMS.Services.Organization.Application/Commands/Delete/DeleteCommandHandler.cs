using MediatR;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Commands.Delete;

public class DeleteCommandHandler : IRequestHandler<DeleteCommand, bool>
{
    private readonly IOrganizationDbContext _context;

    public DeleteCommandHandler(IOrganizationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<EntityType>(request.EntityType, true, out var entityType))
            throw new ArgumentException($"Invalid entity type: {request.EntityType}");

        switch (entityType)
        {
            case EntityType.Company:
                var company = await _context.Companies
                    .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
                if (company == null) return false;
                company.Deactivate();
                break;

            case EntityType.Branch:
                var branch = await _context.Branches
                    .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
                if (branch == null) return false;
                branch.Deactivate();
                break;

            case EntityType.Department:
                var department = await _context.Departments
                    .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);
                if (department == null) return false;
                department.Deactivate();
                break;

            case EntityType.Designation:
                var designation = await _context.Designations
                    .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);
                if (designation == null) return false;
                designation.Deactivate();
                break;

            case EntityType.Grade:
                var grade = await _context.Grades
                    .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
                if (grade == null) return false;
                grade.Deactivate();
                break;

            case EntityType.Shift:
                var shift = await _context.Shifts
                    .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
                if (shift == null) return false;
                shift.Deactivate();
                break;

            case EntityType.Holiday:
                var holiday = await _context.Holidays
                    .FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);
                if (holiday == null) return false;
                holiday.Deactivate();
                break;

            case EntityType.CompanyPolicy:
                var policy = await _context.CompanyPolicies
                    .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
                if (policy == null) return false;
                policy.Deactivate();
                break;

            default:
                throw new ArgumentException($"Entity type {request.EntityType} is not supported.");
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
