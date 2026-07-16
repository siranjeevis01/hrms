using HRMS.Services.ProjectTask.Application.Interfaces;
using HRMS.Services.ProjectTask.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.LogTime;

public class LogTimeCommandHandler : IRequestHandler<LogTimeCommand, Guid>
{
    private readonly IProjectTaskDbContext _context;

    public LogTimeCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(LogTimeCommand request, CancellationToken cancellationToken)
    {
        var timeLog = TimeLog.Create(
            request.TaskItemId,
            request.StoryId,
            request.EmployeeId,
            request.Hours,
            request.Date,
            request.Description,
            request.TenantId);

        if (request.TaskItemId.HasValue)
        {
            var task = await _context.TaskItems
                .FirstOrDefaultAsync(t => t.Id == request.TaskItemId.Value, cancellationToken);
            if (task != null)
                task.AddLoggedHours(request.Hours);
        }

        _context.TimeLogs.Add(timeLog);
        await _context.SaveChangesAsync(cancellationToken);

        return timeLog.Id;
    }
}
