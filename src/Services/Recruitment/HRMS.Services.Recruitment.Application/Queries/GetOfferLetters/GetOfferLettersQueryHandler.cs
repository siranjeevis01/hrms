using AutoMapper;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Recruitment.Application.Queries.GetOfferLetters;

public class GetOfferLettersQueryHandler : IRequestHandler<GetOfferLettersQuery, PagedResult<OfferLetterDto>>
{
    private readonly IRecruitmentDbContext _context;
    private readonly IMapper _mapper;

    public GetOfferLettersQueryHandler(IRecruitmentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<OfferLetterDto>> Handle(GetOfferLettersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.OfferLetters
            .Include(o => o.Candidate)
            .AsQueryable();

        if (request.CandidateId.HasValue)
            query = query.Where(o => o.CandidateId == request.CandidateId.Value);

        if (request.TenantId.HasValue)
            query = query.Where(o => o.TenantId == request.TenantId.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var offers = await query
            .OrderByDescending(o => o.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<OfferLetterDto>>(offers);

        return PagedResult<OfferLetterDto>.Create(items, totalCount, request.PageNumber, request.PageSize);
    }
}
