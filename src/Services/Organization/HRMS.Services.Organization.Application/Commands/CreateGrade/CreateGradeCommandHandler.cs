using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Commands.CreateGrade;

public class CreateGradeCommandHandler : IRequestHandler<CreateGradeCommand, GradeDto>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public CreateGradeCommandHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GradeDto> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
    {
        var companyExists = await _context.Companies
            .AnyAsync(c => c.Id == request.CompanyId && c.IsActive, cancellationToken);

        if (!companyExists)
            throw new InvalidOperationException($"Active company with ID {request.CompanyId} not found.");

        var grade = Grade.Create(
            request.CompanyId,
            request.Name,
            request.Code,
            request.MinSalary,
            request.MaxSalary,
            request.TenantId,
            request.Benefits);

        _context.Grades.Add(grade);
        await _context.SaveChangesAsync(cancellationToken);

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == request.CompanyId, cancellationToken);

        var dto = _mapper.Map<GradeDto>(grade);
        dto.CompanyName = company?.Name ?? string.Empty;
        return dto;
    }
}
