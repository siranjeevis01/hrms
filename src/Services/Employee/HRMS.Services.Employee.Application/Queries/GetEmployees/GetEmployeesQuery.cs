using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Domain.Enums;
using MediatR;

namespace HRMS.Services.Employee.Application.Queries.GetEmployees;

public class GetEmployeesQuery : IRequest<PagedResult<EmployeeListDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? DepartmentId { get; set; }
    public Guid? DesignationId { get; set; }
    public EmploymentStatus? Status { get; set; }
    public string? SearchTerm { get; set; }
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}
