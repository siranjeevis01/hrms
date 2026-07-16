using AutoMapper;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Recruitment.Application.Queries.GetCandidates;

public class GetCandidatesQueryHandler : IRequestHandler<GetCandidatesQuery, PagedResult<CandidateDto>>
{
    private readonly IRecruitmentDbContext _context;
    private readonly IMapper _mapper;

    public GetCandidatesQueryHandler(IRecruitmentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<CandidateDto>> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Candidates.AsQueryable();

        if (request.Status.HasValue)
            query = query.Where(c => c.Status == request.Status.Value);

        if (request.Source.HasValue)
            query = query.Where(c => c.Source == request.Source.Value);

        if (request.TenantId.HasValue)
            query = query.Where(c => c.TenantId == request.TenantId.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.ToLower();
            query = query.Where(c =>
                c.FirstName.ToLower().Contains(search) ||
                c.LastName.ToLower().Contains(search) ||
                c.Email.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var candidates = await query
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<CandidateDto>>(candidates);

        return PagedResult<CandidateDto>.Create(items, totalCount, request.PageNumber, request.PageSize);
    }
}
