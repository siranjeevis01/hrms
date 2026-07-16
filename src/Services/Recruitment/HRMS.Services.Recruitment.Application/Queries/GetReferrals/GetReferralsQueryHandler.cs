using AutoMapper;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Interfaces;
using HRMS.Services.Recruitment.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Queries.GetReferrals;

public class GetReferralsQueryHandler : IRequestHandler<GetReferralsQuery, List<CandidateDto>>
{
    private readonly IRecruitmentDbContext _context;
    private readonly IMapper _mapper;

    public GetReferralsQueryHandler(IRecruitmentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CandidateDto>> Handle(GetReferralsQuery request, CancellationToken cancellationToken)
    {
        var candidates = await _context.Candidates
            .Where(c => c.ReferralEmployeeId == request.EmployeeId && c.Source == CandidateSource.Referral)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<CandidateDto>>(candidates);
    }
}
