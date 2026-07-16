using AutoMapper;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Queries.GetCandidate;

public class GetCandidateQueryHandler : IRequestHandler<GetCandidateQuery, CandidateDto?>
{
    private readonly IRecruitmentDbContext _context;
    private readonly IMapper _mapper;

    public GetCandidateQueryHandler(IRecruitmentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CandidateDto?> Handle(GetCandidateQuery request, CancellationToken cancellationToken)
    {
        var candidate = await _context.Candidates
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (candidate == null)
            return null;

        return _mapper.Map<CandidateDto>(candidate);
    }
}
