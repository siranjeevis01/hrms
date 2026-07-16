using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.UpdateEpic;

public class UpdateEpicCommandHandler : IRequestHandler<UpdateEpicCommand>
{
    private readonly IProjectTaskDbContext _context;

    public UpdateEpicCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateEpicCommand request, CancellationToken cancellationToken)
    {
        var epic = await _context.Epics
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Epic with ID {request.Id} not found.");

        epic.Update(request.Title, request.Description, request.Priority, request.StartDate, request.TargetDate);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
