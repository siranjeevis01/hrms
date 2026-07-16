using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace HRMS.Services.Performance.Application.Queries.GetOKR;

public class GetOKRQueryHandler : IRequestHandler<GetOKRQuery, OKRDto?>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetOKRQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OKRDto?> Handle(GetOKRQuery request, CancellationToken cancellationToken)
    {
        var okr = await _context.OKRs
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

        if (okr == null)
            return null;

        return _mapper.Map<OKRDto>(okr);
    }
}
