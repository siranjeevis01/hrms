using AutoMapper;
using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeEducation;

public class GetEmployeeEducationQueryHandler : IRequestHandler<GetEmployeeEducationQuery, List<EducationDto>>
{
    private readonly IEmployeeDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeEducationQueryHandler(IEmployeeDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EducationDto>> Handle(GetEmployeeEducationQuery request, CancellationToken cancellationToken)
    {
        var educations = await _context.Educations
            .Where(e => e.EmployeeId == request.EmployeeId)
            .OrderByDescending(e => e.IsHighest)
            .ThenByDescending(e => e.EndDate)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<EducationDto>>(educations);
    }
}
