using HRMS.Services.Training.Application.DTOs;
using MediatR;

namespace HRMS.Services.Training.Application.Queries.GetCourseModules;

public class GetCourseModulesQuery : IRequest<List<CourseModuleDto>>
{
    public Guid CourseId { get; set; }
}
