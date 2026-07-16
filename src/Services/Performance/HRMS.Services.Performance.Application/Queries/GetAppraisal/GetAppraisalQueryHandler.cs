using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetAppraisal;

public class GetAppraisalQueryHandler : IRequestHandler<GetAppraisalQuery, AppraisalDto?>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetAppraisalQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AppraisalDto?> Handle(GetAppraisalQuery request, CancellationToken cancellationToken)
    {
        var appraisal = await _context.Appraisals
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (appraisal == null)
            return null;

        return _mapper.Map<AppraisalDto>(appraisal);
    }
}
