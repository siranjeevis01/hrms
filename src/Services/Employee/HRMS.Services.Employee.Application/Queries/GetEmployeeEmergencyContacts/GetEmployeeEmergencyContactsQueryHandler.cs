using AutoMapper;
using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeEmergencyContacts;

public class GetEmployeeEmergencyContactsQueryHandler : IRequestHandler<GetEmployeeEmergencyContactsQuery, List<EmergencyContactDto>>
{
    private readonly IEmployeeDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeEmergencyContactsQueryHandler(IEmployeeDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EmergencyContactDto>> Handle(GetEmployeeEmergencyContactsQuery request, CancellationToken cancellationToken)
    {
        var contacts = await _context.EmergencyContacts
            .Where(c => c.EmployeeId == request.EmployeeId)
            .OrderByDescending(c => c.IsPrimary)
            .ThenBy(c => c.Name)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<EmergencyContactDto>>(contacts);
    }
}
