using AutoMapper;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetCertificate;

public class GetCertificateQueryHandler : IRequestHandler<GetCertificateQuery, CertificateDto>
{
    private readonly ITrainingDbContext _context;
    private readonly IMapper _mapper;

    public GetCertificateQueryHandler(ITrainingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CertificateDto> Handle(GetCertificateQuery request, CancellationToken cancellationToken)
    {
        var certificate = await _context.Certificates
            .FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted, cancellationToken);

        if (certificate == null) return null;

        var dto = _mapper.Map<CertificateDto>(certificate);

        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.Id == certificate.CourseId, cancellationToken);
        dto.CourseTitle = course?.Title;

        return dto;
    }
}
