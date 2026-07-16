using HRMS.Services.ProjectTask.Application.DTOs;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Queries.GetSprint;

public class GetSprintQuery : IRequest<SprintDto?>
{
    public Guid Id { get; set; }
}
