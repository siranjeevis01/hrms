using AutoMapper;
using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Queries.GetTicketCategories;

public class GetTicketCategoriesQueryHandler : IRequestHandler<GetTicketCategoriesQuery, List<TicketCategoryDto>>
{
    private readonly IHelpdeskDbContext _context;
    private readonly IMapper _mapper;

    public GetTicketCategoriesQueryHandler(IHelpdeskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TicketCategoryDto>> Handle(GetTicketCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.TicketCategories
            .Where(c => c.TenantId == request.TenantId && c.IsActive)
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<TicketCategoryDto>>(categories);
    }
}
