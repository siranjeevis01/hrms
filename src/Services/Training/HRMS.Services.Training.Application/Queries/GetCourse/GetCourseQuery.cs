using HRMS.Services.Training.Application.DTOs;
using MediatR;

namespace HRMS.Services.Training.Application.Queries.GetCourse;

public class GetCourseQuery : IRequest<CourseDto>
{
    public Guid Id { get; set; }
}
