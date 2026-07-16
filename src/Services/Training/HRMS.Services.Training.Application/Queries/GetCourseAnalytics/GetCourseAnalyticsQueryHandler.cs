using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using HRMS.Services.Training.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetCourseAnalytics;

public class GetCourseAnalyticsQueryHandler : IRequestHandler<GetCourseAnalyticsQuery, CourseAnalyticsDto>
{
    private readonly ITrainingDbContext _context;

    public GetCourseAnalyticsQueryHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task<CourseAnalyticsDto> Handle(GetCourseAnalyticsQuery request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.Id == request.CourseId && !c.IsDeleted, cancellationToken);

        if (course == null) return null;

        var enrollments = await _context.Enrollments
            .Where(e => e.CourseId == request.CourseId && !e.IsDeleted)
            .ToListAsync(cancellationToken);

        var totalEnrollments = enrollments.Count;
        var activeEnrollments = enrollments.Count(e => e.Status == EnrollmentStatus.Enrolled || e.Status == EnrollmentStatus.InProgress);
        var completedEnrollments = enrollments.Count(e => e.Status == EnrollmentStatus.Completed);
        var droppedEnrollments = enrollments.Count(e => e.Status == EnrollmentStatus.Dropped);

        var certificatesIssued = await _context.Certificates
            .CountAsync(c => c.CourseId == request.CourseId && !c.IsDeleted, cancellationToken);

        var completionRate = totalEnrollments > 0
            ? (double)completedEnrollments / totalEnrollments * 100
            : 0;

        var averageProgress = enrollments.Any()
            ? enrollments.Average(e => e.ProgressPercentage)
            : 0;

        return new CourseAnalyticsDto
        {
            CourseId = course.Id,
            CourseTitle = course.Title,
            TotalEnrollments = totalEnrollments,
            ActiveEnrollments = activeEnrollments,
            CompletedEnrollments = completedEnrollments,
            DroppedEnrollments = droppedEnrollments,
            CompletionRate = Math.Round(completionRate, 2),
            AverageProgress = Math.Round(averageProgress, 2),
            CertificatesIssued = certificatesIssued
        };
    }
}
