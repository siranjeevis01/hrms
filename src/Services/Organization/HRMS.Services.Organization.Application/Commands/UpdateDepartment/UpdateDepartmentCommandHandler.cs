using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Commands.UpdateDepartment;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, DepartmentDto>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateDepartmentCommandHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DepartmentDto> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _context.Departments
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Department with ID {request.Id} not found.");

        DepartmentType? departmentType = null;
        if (!string.IsNullOrWhiteSpace(request.Type) &&
            Enum.TryParse<DepartmentType>(request.Type, true, out var parsedType))
        {
            departmentType = parsedType;
        }

        department.UpdateDetails(
            name: request.Name,
            code: request.Code,
            description: request.Description,
            managerId: request.ManagerId,
            hodId: request.HODId,
            branchId: request.BranchId,
            parentDepartmentId: request.ParentDepartmentId,
            type: departmentType);

        await _context.SaveChangesAsync(cancellationToken);

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == department.CompanyId, cancellationToken);

        var dto = _mapper.Map<DepartmentDto>(department);
        dto.CompanyName = company?.Name ?? string.Empty;
        return dto;
    }
}
