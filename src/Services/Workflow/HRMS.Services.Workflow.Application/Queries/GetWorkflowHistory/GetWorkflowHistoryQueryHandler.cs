using AutoMapper;
using HRMS.Services.Workflow.Application.DTOs;
using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Queries.GetWorkflowHistory;

public class GetWorkflowHistoryQueryHandler : IRequestHandler<GetWorkflowHistoryQuery, List<WorkflowActionDto>>
{
    private readonly IWorkflowDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkflowHistoryQueryHandler(IWorkflowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<WorkflowActionDto>> Handle(GetWorkflowHistoryQuery request, CancellationToken cancellationToken)
    {
        var actions = await _context.WorkflowActions
            .Where(a => a.WorkflowInstanceId == request.WorkflowInstanceId)
            .OrderBy(a => a.ActionedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<WorkflowActionDto>>(actions);
    }
}
