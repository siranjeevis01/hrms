using AutoMapper;
using HRMS.Services.Workflow.Application.DTOs;
using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Queries.GetWorkflowActions;

public class GetWorkflowActionsQueryHandler : IRequestHandler<GetWorkflowActionsQuery, List<WorkflowActionDto>>
{
    private readonly IWorkflowDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkflowActionsQueryHandler(IWorkflowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<WorkflowActionDto>> Handle(GetWorkflowActionsQuery request, CancellationToken cancellationToken)
    {
        var actions = await _context.WorkflowActions
            .Where(a => a.WorkflowInstanceId == request.WorkflowInstanceId)
            .OrderBy(a => a.ActionedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<WorkflowActionDto>>(actions);
    }
}
