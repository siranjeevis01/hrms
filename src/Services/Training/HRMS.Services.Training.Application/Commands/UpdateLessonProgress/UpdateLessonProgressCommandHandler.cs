using HRMS.Services.Training.Application.Interfaces;
using HRMS.Services.Training.Domain.Entities;
using HRMS.Services.Training.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Commands.UpdateLessonProgress;

public class UpdateLessonProgressCommandHandler : IRequestHandler<UpdateLessonProgressCommand>
{
    private readonly ITrainingDbContext _context;

    public UpdateLessonProgressCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateLessonProgressCommand request, CancellationToken cancellationToken)
    {
        var progress = await _context.LessonProgresses
            .FirstOrDefaultAsync(lp => lp.EnrollmentId == request.EnrollmentId && lp.LessonId == request.LessonId, cancellationToken);

        if (progress == null)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.Id == request.EnrollmentId, cancellationToken);

            if (enrollment == null)
                throw new KeyNotFoundException($"Enrollment with Id {request.EnrollmentId} not found.");

            progress = LessonProgress.Create(request.EnrollmentId, request.LessonId, enrollment.TenantId);
            _context.LessonProgresses.Add(progress);
        }

        if (request.Status == LessonProgressStatus.InProgress.ToString())
        {
            progress.Start();
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.Id == request.EnrollmentId, cancellationToken);
            enrollment?.MarkInProgress();
        }
        else if (request.Status == LessonProgressStatus.Completed.ToString())
        {
            progress.Complete();

            var totalLessons = await _context.Lessons
                .CountAsync(l => l.ModuleId == _context.LessonProgresses
                    .Where(lp => lp.EnrollmentId == request.EnrollmentId)
                    .Select(lp => lp.LessonId)
                    .FirstOrDefault(), cancellationToken);

            var completedLessons = await _context.LessonProgresses
                .CountAsync(lp => lp.EnrollmentId == request.EnrollmentId && lp.Status == LessonProgressStatus.Completed, cancellationToken);

            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.Id == request.EnrollmentId, cancellationToken);

            if (enrollment != null && totalLessons > 0)
            {
                var percentage = ((double)completedLessons / totalLessons) * 100;
                enrollment.UpdateProgress(percentage);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
