using HRMS.Services.Training.Application.DTOs;
using MediatR;

namespace HRMS.Services.Training.Application.Queries.GetCourseAnalytics;

public class GetCourseAnalyticsQuery : IRequest<CourseAnalyticsDto?>
{
    public Guid CourseId { get; set; }
}
