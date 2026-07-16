using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using HRMS.Services.Training.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetTrainingStats;

public class GetTrainingStatsQueryHandler : IRequestHandler<GetTrainingStatsQuery, TrainingStatsDto>
{
    private readonly ITrainingDbContext _context;

    public GetTrainingStatsQueryHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task<TrainingStatsDto> Handle(GetTrainingStatsQuery request, CancellationToken cancellationToken)
    {
        var totalCourses = await _context.Courses.CountAsync(c => !c.IsDeleted, cancellationToken);
        var publishedCourses = await _context.Courses.CountAsync(c => c.Status == CourseStatus.Published && !c.IsDeleted, cancellationToken);

        var enrollments = await _context.Enrollments.Where(e => !e.IsDeleted).ToListAsync(cancellationToken);
        var totalEnrollments = enrollments.Count;
        var activeEnrollments = enrollments.Count(e => e.Status == EnrollmentStatus.Enrolled || e.Status == EnrollmentStatus.InProgress);
        var completedEnrollments = enrollments.Count(e => e.Status == EnrollmentStatus.Completed);

        var totalAssessments = await _context.Assessments.CountAsync(a => !a.IsDeleted, cancellationToken);
        var certificatesIssued = await _context.Certificates.CountAsync(c => !c.IsDeleted, cancellationToken);

        var averageCompletionRate = totalEnrollments > 0
            ? (double)completedEnrollments / totalEnrollments * 100
            : 0;

        var averageProgress = enrollments.Any()
            ? enrollments.Average(e => e.ProgressPercentage)
            : 0;

        return new TrainingStatsDto
        {
            TotalCourses = totalCourses,
            PublishedCourses = publishedCourses,
            TotalEnrollments = totalEnrollments,
            ActiveEnrollments = activeEnrollments,
            CompletedEnrollments = completedEnrollments,
            TotalAssessments = totalAssessments,
            CertificatesIssued = certificatesIssued,
            AverageCompletionRate = Math.Round(averageCompletionRate, 2),
            AverageProgressPercentage = Math.Round(averageProgress, 2)
        };
    }
}
