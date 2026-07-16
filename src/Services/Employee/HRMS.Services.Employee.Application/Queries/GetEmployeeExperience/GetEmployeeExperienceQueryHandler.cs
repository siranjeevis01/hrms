using AutoMapper;
using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeExperience;

public class GetEmployeeExperienceQueryHandler : IRequestHandler<GetEmployeeExperienceQuery, List<WorkExperienceDto>>
{
    private readonly IEmployeeDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeExperienceQueryHandler(IEmployeeDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<WorkExperienceDto>> Handle(GetEmployeeExperienceQuery request, CancellationToken cancellationToken)
    {
        var experiences = await _context.WorkExperiences
            .Where(e => e.EmployeeId == request.EmployeeId)
            .OrderByDescending(e => e.StartDate)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<WorkExperienceDto>>(experiences);
    }
}
