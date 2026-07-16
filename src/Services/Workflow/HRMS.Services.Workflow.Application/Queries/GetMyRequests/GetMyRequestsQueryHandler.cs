using AutoMapper;
using HRMS.Services.Workflow.Application.DTOs;
using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Queries.GetMyRequests;

public class GetMyRequestsQueryHandler : IRequestHandler<GetMyRequestsQuery, List<WorkflowInstanceDto>>
{
    private readonly IWorkflowDbContext _context;
    private readonly IMapper _mapper;

    public GetMyRequestsQueryHandler(IWorkflowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<WorkflowInstanceDto>> Handle(GetMyRequestsQuery request, CancellationToken cancellationToken)
    {
        var instances = await _context.WorkflowInstances
            .Where(i => i.RequestedById == request.RequestedById)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<WorkflowInstanceDto>>(instances);
    }
}
