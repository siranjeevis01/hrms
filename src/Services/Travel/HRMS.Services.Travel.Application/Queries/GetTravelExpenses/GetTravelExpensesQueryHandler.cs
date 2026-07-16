using AutoMapper;
using HRMS.Services.Travel.Application.DTOs;
using HRMS.Services.Travel.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Queries.GetTravelExpenses;

public class GetTravelExpensesQueryHandler : IRequestHandler<GetTravelExpensesQuery, List<TravelExpenseDto>>
{
    private readonly ITravelDbContext _context;
    private readonly IMapper _mapper;

    public GetTravelExpensesQueryHandler(ITravelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TravelExpenseDto>> Handle(GetTravelExpensesQuery request, CancellationToken cancellationToken)
    {
        var expenses = await _context.TravelExpenses
            .Where(e => e.TravelRequestId == request.TravelRequestId)
            .OrderBy(e => e.Date)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<TravelExpenseDto>>(expenses);
    }
}
