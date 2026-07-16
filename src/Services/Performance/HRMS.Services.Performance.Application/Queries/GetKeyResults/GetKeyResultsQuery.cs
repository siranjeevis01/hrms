using HRMS.Services.Performance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetKeyResults;

public class GetKeyResultsQuery : IRequest<List<KeyResultDto>>
{
    public Guid GoalId { get; set; }
}
