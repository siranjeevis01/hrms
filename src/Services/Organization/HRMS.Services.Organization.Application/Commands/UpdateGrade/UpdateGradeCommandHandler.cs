using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Commands.UpdateGrade;

public class UpdateGradeCommandHandler : IRequestHandler<UpdateGradeCommand, GradeDto>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateGradeCommandHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GradeDto> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
    {
        var grade = await _context.Grades
            .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Grade with ID {request.Id} not found.");

        grade.UpdateDetails(
            name: request.Name,
            code: request.Code,
            minSalary: request.MinSalary,
            maxSalary: request.MaxSalary,
            benefits: request.Benefits);

        await _context.SaveChangesAsync(cancellationToken);

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == grade.CompanyId, cancellationToken);

        var dto = _mapper.Map<GradeDto>(grade);
        dto.CompanyName = company?.Name ?? string.Empty;
        return dto;
    }
}
