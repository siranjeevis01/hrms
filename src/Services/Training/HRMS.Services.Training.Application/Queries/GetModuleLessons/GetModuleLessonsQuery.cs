using HRMS.Services.Training.Application.DTOs;
using MediatR;

namespace HRMS.Services.Training.Application.Queries.GetModuleLessons;

public class GetModuleLessonsQuery : IRequest<List<LessonDto>>
{
    public Guid ModuleId { get; set; }
}
