using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.UpdateTimeLog;

public class UpdateTimeLogCommandHandler : IRequestHandler<UpdateTimeLogCommand>
{
    private readonly IProjectTaskDbContext _context;

    public UpdateTimeLogCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTimeLogCommand request, CancellationToken cancellationToken)
    {
        var timeLog = await _context.TimeLogs
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"TimeLog with ID {request.Id} not found.");

        timeLog.Update(request.Hours, request.Date, request.Description);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
