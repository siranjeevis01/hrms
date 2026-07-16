using AutoMapper;
using HRMS.Services.Workflow.Application.DTOs;
using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Queries.GetWorkflowSteps;

public class GetWorkflowStepsQueryHandler : IRequestHandler<GetWorkflowStepsQuery, List<WorkflowStepDto>>
{
    private readonly IWorkflowDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkflowStepsQueryHandler(IWorkflowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<WorkflowStepDto>> Handle(GetWorkflowStepsQuery request, CancellationToken cancellationToken)
    {
        var steps = await _context.WorkflowSteps
            .Where(s => s.WorkflowDefinitionId == request.WorkflowDefinitionId)
            .OrderBy(s => s.StepNumber)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<WorkflowStepDto>>(steps);
    }
}
