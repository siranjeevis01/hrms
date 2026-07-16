using HRMS.Services.Training.Application.Interfaces;
using HRMS.Services.Training.Domain.Entities;
using MediatR;

namespace HRMS.Services.Training.Application.Commands.CreateTrainingSchedule;

public class CreateTrainingScheduleCommandHandler : IRequestHandler<CreateTrainingScheduleCommand, Guid>
{
    private readonly ITrainingDbContext _context;

    public CreateTrainingScheduleCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateTrainingScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = TrainingSchedule.Create(
            request.CourseId,
            request.StartDate,
            request.EndDate,
            request.Location,
            request.MaxParticipants,
            request.InstructorName,
            request.MeetingUrl,
            request.TenantId);

        _context.TrainingSchedules.Add(schedule);
        await _context.SaveChangesAsync(cancellationToken);

        return schedule.Id;
    }
}
