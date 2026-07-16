using AutoMapper;
using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeCertifications;

public class GetEmployeeCertificationsQueryHandler : IRequestHandler<GetEmployeeCertificationsQuery, List<CertificationDto>>
{
    private readonly IEmployeeDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeCertificationsQueryHandler(IEmployeeDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CertificationDto>> Handle(GetEmployeeCertificationsQuery request, CancellationToken cancellationToken)
    {
        var certifications = await _context.Certifications
            .Where(c => c.EmployeeId == request.EmployeeId)
            .OrderByDescending(c => c.IssueDate)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<CertificationDto>>(certifications);
    }
}
