using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace HRMS.Services.Performance.Application.Queries.GetFeedback360;

public class GetFeedback360QueryHandler : IRequestHandler<GetFeedback360Query, Feedback360Dto?>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetFeedback360QueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Feedback360Dto?> Handle(GetFeedback360Query request, CancellationToken cancellationToken)
    {
        var feedback = await _context.Feedback360s
            .Include(f => f.Answers)
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (feedback == null)
            return null;

        return _mapper.Map<Feedback360Dto>(feedback);
    }
}
