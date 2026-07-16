using HRMS.Services.Workflow.Application.DTOs;
using MediatR;

namespace HRMS.Services.Workflow.Application.Queries.GetDelegates;

public class GetDelegatesQuery : IRequest<List<DelegateDto>>
{
    public Guid? UserId { get; set; }
}
