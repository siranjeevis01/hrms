using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Events;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Domain.Entities;
using HRMS.Services.Organization.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Commands.CreateDepartment;

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;

    public CreateDepartmentCommandHandler(
        IOrganizationDbContext context,
        IMapper mapper,
        IPublisher publisher)
    {
        _context = context;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<DepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var companyExists = await _context.Companies
            .AnyAsync(c => c.Id == request.CompanyId && c.IsActive, cancellationToken);

        if (!companyExists)
            throw new InvalidOperationException($"Active company with ID {request.CompanyId} not found.");

        if (request.ParentDepartmentId.HasValue)
        {
            var parentExists = await _context.Departments
                .AnyAsync(d => d.Id == request.ParentDepartmentId.Value && d.IsActive, cancellationToken);

            if (!parentExists)
                throw new InvalidOperationException($"Parent department with ID {request.ParentDepartmentId} not found.");
        }

        if (request.BranchId.HasValue)
        {
            var branchExists = await _context.Branches
                .AnyAsync(b => b.Id == request.BranchId.Value && b.IsActive, cancellationToken);

            if (!branchExists)
                throw new InvalidOperationException($"Branch with ID {request.BranchId} not found.");
        }

        var departmentType = Enum.TryParse<DepartmentType>(request.Type, true, out var parsedType)
            ? parsedType
            : DepartmentType.Functional;

        var department = Department.Create(
            request.CompanyId,
            request.Name,
            request.Code,
            request.TenantId,
            request.BranchId,
            request.ParentDepartmentId,
            departmentType);

        department.UpdateDetails(
            description: request.Description,
            managerId: request.ManagerId,
            hodId: request.HODId);

        _context.Departments.Add(department);
        await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new DepartmentCreatedEvent
        {
            DepartmentId = department.Id,
            CompanyId = department.CompanyId,
            Name = department.Name,
            Code = department.Code,
            ParentDepartmentId = department.ParentDepartmentId,
            TenantId = department.TenantId
        }, cancellationToken);

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == request.CompanyId, cancellationToken);

        var dto = _mapper.Map<DepartmentDto>(department);
        dto.CompanyName = company?.Name ?? string.Empty;
        return dto;
    }
}
