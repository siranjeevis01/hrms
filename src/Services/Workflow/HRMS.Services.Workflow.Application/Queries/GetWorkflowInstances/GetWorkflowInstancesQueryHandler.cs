using AutoMapper;
using HRMS.Services.Workflow.Application.DTOs;
using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Queries.GetWorkflowInstances;

public class GetWorkflowInstancesQueryHandler : IRequestHandler<GetWorkflowInstancesQuery, PagedResult<WorkflowInstanceDto>>
{
    private readonly IWorkflowDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkflowInstancesQueryHandler(IWorkflowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<WorkflowInstanceDto>> Handle(GetWorkflowInstancesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.WorkflowInstances.AsQueryable();

        if (request.EntityType.HasValue)
            query = query.Where(i => i.EntityType == request.EntityType.Value);

        if (request.Status.HasValue)
            query = query.Where(i => i.Status == request.Status.Value);

        if (request.RequestedById.HasValue)
            query = query.Where(i => i.RequestedById == request.RequestedById.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var instances = await query
            .OrderByDescending(i => i.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<WorkflowInstanceDto>>(instances);

        return new PagedResult<WorkflowInstanceDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
