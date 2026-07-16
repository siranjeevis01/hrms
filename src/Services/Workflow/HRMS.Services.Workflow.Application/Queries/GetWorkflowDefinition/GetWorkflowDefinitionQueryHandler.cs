using AutoMapper;
using HRMS.Services.Workflow.Application.DTOs;
using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Queries.GetWorkflowDefinition;

public class GetWorkflowDefinitionQueryHandler : IRequestHandler<GetWorkflowDefinitionQuery, WorkflowDefinitionDto?>
{
    private readonly IWorkflowDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkflowDefinitionQueryHandler(IWorkflowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<WorkflowDefinitionDto?> Handle(GetWorkflowDefinitionQuery request, CancellationToken cancellationToken)
    {
        var definition = await _context.WorkflowDefinitions
            .Include(d => d.StepsCollection.OrderBy(s => s.StepNumber))
            .Include(d => d.NotificationRules)
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (definition == null)
            return null;

        return _mapper.Map<WorkflowDefinitionDto>(definition);
    }
}
