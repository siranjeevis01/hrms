using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Queries.GetDepartmentTree;

public class GetDepartmentTreeQueryHandler : IRequestHandler<GetDepartmentTreeQuery, List<DepartmentDto>>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetDepartmentTreeQueryHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DepartmentDto>> Handle(GetDepartmentTreeQuery request, CancellationToken cancellationToken)
    {
        var departments = await _context.Departments
            .AsNoTracking()
            .Where(d => d.CompanyId == request.CompanyId && d.IsActive)
            .OrderBy(d => d.Name)
            .ToListAsync(cancellationToken);

        var departmentDtos = _mapper.Map<List<DepartmentDto>>(departments);

        foreach (var dto in departmentDtos)
        {
            var dept = departments.First(d => d.Id == dto.Id);
            dto.CompanyName = string.Empty;
            if (dept.BranchId.HasValue)
                dto.BranchName = null;
        }

        var lookup = departmentDtos.ToLookup(d => d.ParentDepartmentId);

        var rootDepartments = departmentDtos
            .Where(d => !d.ParentDepartmentId.HasValue)
            .ToList();

        BuildTree(rootDepartments, lookup);

        return rootDepartments;
    }

    private static void BuildTree(List<DepartmentDto> parents, ILookup<Guid?, DepartmentDto> lookup)
    {
        foreach (var parent in parents)
        {
            parent.Children = lookup[parent.Id].ToList();
            if (parent.Children.Any())
                BuildTree(parent.Children, lookup);
        }
    }
}
