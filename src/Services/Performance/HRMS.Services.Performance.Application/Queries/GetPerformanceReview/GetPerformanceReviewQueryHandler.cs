using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace HRMS.Services.Performance.Application.Queries.GetPerformanceReview;

public class GetPerformanceReviewQueryHandler : IRequestHandler<GetPerformanceReviewQuery, PerformanceReviewDto?>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetPerformanceReviewQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PerformanceReviewDto?> Handle(GetPerformanceReviewQuery request, CancellationToken cancellationToken)
    {
        var review = await _context.PerformanceReviews
            .Include(r => r.Criteria)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (review == null)
            return null;

        return _mapper.Map<PerformanceReviewDto>(review);
    }
}
