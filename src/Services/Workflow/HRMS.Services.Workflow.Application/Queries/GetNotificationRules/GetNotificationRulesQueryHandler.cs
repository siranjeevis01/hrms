using AutoMapper;
using HRMS.Services.Workflow.Application.DTOs;
using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Queries.GetNotificationRules;

public class GetNotificationRulesQueryHandler : IRequestHandler<GetNotificationRulesQuery, List<NotificationRuleDto>>
{
    private readonly IWorkflowDbContext _context;
    private readonly IMapper _mapper;

    public GetNotificationRulesQueryHandler(IWorkflowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<NotificationRuleDto>> Handle(GetNotificationRulesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.NotificationRules.AsQueryable();

        if (request.WorkflowDefinitionId.HasValue)
            query = query.Where(n => n.WorkflowDefinitionId == request.WorkflowDefinitionId.Value);

        var rules = await query
            .OrderBy(n => n.EventType)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<NotificationRuleDto>>(rules);
    }
}
