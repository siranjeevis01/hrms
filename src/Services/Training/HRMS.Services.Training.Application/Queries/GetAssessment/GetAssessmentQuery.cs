using HRMS.Services.Training.Application.DTOs;
using MediatR;

namespace HRMS.Services.Training.Application.Queries.GetAssessment;

public class GetAssessmentQuery : IRequest<AssessmentDto>
{
    public Guid Id { get; set; }
}
