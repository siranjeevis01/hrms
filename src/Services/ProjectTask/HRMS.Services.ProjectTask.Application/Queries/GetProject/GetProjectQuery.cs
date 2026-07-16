using HRMS.Services.ProjectTask.Application.DTOs;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Queries.GetProject;

public class GetProjectQuery : IRequest<ProjectDto?>
{
    public Guid Id { get; set; }
}
