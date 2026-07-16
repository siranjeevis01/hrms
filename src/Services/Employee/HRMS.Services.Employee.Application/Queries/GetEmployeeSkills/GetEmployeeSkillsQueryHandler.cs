using AutoMapper;
using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeSkills;

public class GetEmployeeSkillsQueryHandler : IRequestHandler<GetEmployeeSkillsQuery, List<SkillDto>>
{
    private readonly IEmployeeDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeSkillsQueryHandler(IEmployeeDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SkillDto>> Handle(GetEmployeeSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills = await _context.Skills
            .Where(s => s.EmployeeId == request.EmployeeId)
            .OrderBy(s => s.Category)
            .ThenBy(s => s.Name)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<SkillDto>>(skills);
    }
}
