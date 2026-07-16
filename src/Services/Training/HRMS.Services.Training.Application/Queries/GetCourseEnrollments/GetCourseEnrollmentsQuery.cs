using HRMS.Services.Training.Application.DTOs;
using MediatR;

namespace HRMS.Services.Training.Application.Queries.GetCourseEnrollments;

public class GetCourseEnrollmentsQuery : IRequest<List<EnrollmentDto>>
{
    public Guid CourseId { get; set; }
}
