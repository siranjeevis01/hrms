using HRMS.Services.Training.Application.DTOs;
using MediatR;

namespace HRMS.Services.Training.Application.Queries.GetLearningPaths;

public class GetLearningPathsQuery : IRequest<List<LearningPathDto>>
{
    public Guid? DepartmentId { get; set; }
}
