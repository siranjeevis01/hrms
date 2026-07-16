using AutoMapper;
using HRMS.Services.Workflow.Application.DTOs;
using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Queries.GetWorkflowInstance;

public class GetWorkflowInstanceQueryHandler : IRequestHandler<GetWorkflowInstanceQuery, WorkflowInstanceDto?>
{
    private readonly IWorkflowDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkflowInstanceQueryHandler(IWorkflowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<WorkflowInstanceDto?> Handle(GetWorkflowInstanceQuery request, CancellationToken cancellationToken)
    {
        var instance = await _context.WorkflowInstances
            .Include(i => i.Actions)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (instance == null)
            return null;

        var dto = _mapper.Map<WorkflowInstanceDto>(instance);

        var definition = await _context.WorkflowDefinitions
            .FirstOrDefaultAsync(d => d.Id == instance.WorkflowDefinitionId, cancellationToken);

        if (definition != null)
            dto.WorkflowDefinitionName = definition.Name;

        return dto;
    }
}
