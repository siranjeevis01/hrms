using HRMS.Services.Training.Application.DTOs;
using MediatR;

namespace HRMS.Services.Training.Application.Queries.GetLearningPath;

public class GetLearningPathQuery : IRequest<LearningPathDto?>
{
    public Guid Id { get; set; }
}
