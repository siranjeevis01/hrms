using AutoMapper;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Queries.GetOnboardingChecklist;

public class GetOnboardingChecklistQueryHandler : IRequestHandler<GetOnboardingChecklistQuery, List<OnboardingChecklistDto>>
{
    private readonly IRecruitmentDbContext _context;
    private readonly IMapper _mapper;

    public GetOnboardingChecklistQueryHandler(IRecruitmentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OnboardingChecklistDto>> Handle(GetOnboardingChecklistQuery request, CancellationToken cancellationToken)
    {
        var checklists = await _context.OnboardingChecklists
            .Where(o => o.EmployeeId == request.EmployeeId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<OnboardingChecklistDto>>(checklists);
    }
}
