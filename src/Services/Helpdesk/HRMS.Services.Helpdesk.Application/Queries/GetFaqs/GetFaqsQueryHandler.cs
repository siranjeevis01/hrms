using AutoMapper;
using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Queries.GetFaqs;

public class GetFaqsQueryHandler : IRequestHandler<GetFaqsQuery, List<FaqDto>>
{
    private readonly IHelpdeskDbContext _context;
    private readonly IMapper _mapper;

    public GetFaqsQueryHandler(IHelpdeskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<FaqDto>> Handle(GetFaqsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Faqs
            .Where(f => f.TenantId == request.TenantId && f.IsActive);

        if (request.CategoryId.HasValue)
            query = query.Where(f => f.CategoryId == request.CategoryId.Value);

        var faqs = await query
            .OrderBy(f => f.Order)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<FaqDto>>(faqs);
    }
}
