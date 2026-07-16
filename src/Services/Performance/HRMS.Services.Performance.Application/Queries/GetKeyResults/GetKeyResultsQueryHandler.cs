using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetKeyResults;

public class GetKeyResultsQueryHandler : IRequestHandler<GetKeyResultsQuery, List<KeyResultDto>>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetKeyResultsQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<KeyResultDto>> Handle(GetKeyResultsQuery request, CancellationToken cancellationToken)
    {
        var keyResults = await _context.KeyResults
            .Where(kr => kr.GoalId == request.GoalId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<KeyResultDto>>(keyResults);
    }
}
