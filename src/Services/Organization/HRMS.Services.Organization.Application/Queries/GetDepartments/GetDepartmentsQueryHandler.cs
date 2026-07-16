using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Queries.GetDepartments;

public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, List<DepartmentDto>>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetDepartmentsQueryHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DepartmentDto>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Departments
            .AsNoTracking()
            .AsQueryable();

        if (request.CompanyId.HasValue)
            query = query.Where(d => d.CompanyId == request.CompanyId.Value);

        if (request.BranchId.HasValue)
            query = query.Where(d => d.BranchId == request.BranchId.Value);

        if (request.IsActive.HasValue)
            query = query.Where(d => d.IsActive == request.IsActive.Value);

        var departments = await query
            .OrderBy(d => d.Name)
            .ToListAsync(cancellationToken);

        var departmentDtos = _mapper.Map<List<DepartmentDto>>(departments);

        foreach (var dto in departmentDtos)
        {
            dto.CompanyName = string.Empty;
            if (dto.BranchId.HasValue)
                dto.BranchName = null;
            if (dto.ParentDepartmentId.HasValue)
                dto.ParentDepartmentName = departments.FirstOrDefault(d => d.Id == dto.ParentDepartmentId)?.Name;
        }

        return departmentDtos;
    }
}
