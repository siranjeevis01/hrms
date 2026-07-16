using AutoMapper;
using HRMS.Services.Workflow.Application.DTOs;
using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Queries.GetDelegates;

public class GetDelegatesQueryHandler : IRequestHandler<GetDelegatesQuery, List<DelegateDto>>
{
    private readonly IWorkflowDbContext _context;
    private readonly IMapper _mapper;

    public GetDelegatesQueryHandler(IWorkflowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DelegateDto>> Handle(GetDelegatesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Delegates.AsQueryable();

        if (request.UserId.HasValue)
            query = query.Where(d => d.UserId == request.UserId.Value);

        var delegates = await query
            .OrderBy(d => d.StartDate)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<DelegateDto>>(delegates);
    }
}
