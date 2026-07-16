using HRMS.Services.Training.Application.DTOs;
using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Training.Application.Queries.GetCourses;

public class GetCoursesQuery : IRequest<PagedResult<CourseDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public string? Category { get; set; }
    public string? Status { get; set; }
    public Guid? DepartmentId { get; set; }
}
