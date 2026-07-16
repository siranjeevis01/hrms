using AutoMapper;
using HRMS.Services.Workflow.Application.DTOs;
using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Queries.GetWorkflowDefinitions;

public class GetWorkflowDefinitionsQueryHandler : IRequestHandler<GetWorkflowDefinitionsQuery, PagedResult<WorkflowDefinitionDto>>
{
    private readonly IWorkflowDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkflowDefinitionsQueryHandler(IWorkflowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<WorkflowDefinitionDto>> Handle(GetWorkflowDefinitionsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.WorkflowDefinitions.AsQueryable();

        if (request.EntityType.HasValue)
            query = query.Where(d => d.EntityType == request.EntityType.Value);

        if (request.IsActive.HasValue)
            query = query.Where(d => d.IsActive == request.IsActive.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.ToLower();
            query = query.Where(d =>
                d.Name.ToLower().Contains(search) ||
                (d.Description != null && d.Description.ToLower().Contains(search)));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var definitions = await query
            .OrderByDescending(d => d.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<WorkflowDefinitionDto>>(definitions);

        return new PagedResult<WorkflowDefinitionDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
