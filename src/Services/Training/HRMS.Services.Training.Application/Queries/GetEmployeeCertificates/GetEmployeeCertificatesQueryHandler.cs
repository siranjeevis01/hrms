using AutoMapper;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetEmployeeCertificates;

public class GetEmployeeCertificatesQueryHandler : IRequestHandler<GetEmployeeCertificatesQuery, List<CertificateDto>>
{
    private readonly ITrainingDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeCertificatesQueryHandler(ITrainingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CertificateDto>> Handle(GetEmployeeCertificatesQuery request, CancellationToken cancellationToken)
    {
        var certificates = await _context.Certificates
            .Where(c => c.EmployeeId == request.EmployeeId && !c.IsDeleted)
            .OrderByDescending(c => c.IssuedAt)
            .ToListAsync(cancellationToken);

        var dtos = new List<CertificateDto>();
        foreach (var cert in certificates)
        {
            var dto = _mapper.Map<CertificateDto>(cert);
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == cert.CourseId, cancellationToken);
            dto.CourseTitle = course?.Title;
            dtos.Add(dto);
        }
        return dtos;
    }
}
