using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.UpdateWorkExperience;

public class UpdateWorkExperienceCommandHandler : IRequestHandler<UpdateWorkExperienceCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public UpdateWorkExperienceCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateWorkExperienceCommand request, CancellationToken cancellationToken)
    {
        var experience = await _context.WorkExperiences
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Work experience with ID {request.Id} not found.");

        experience.Update(
            request.CompanyName, request.Designation, request.StartDate,
            request.EndDate, request.Description, request.IsCurrent,
            request.ReasonForLeaving, request.ReferenceName, request.ReferencePhone);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
