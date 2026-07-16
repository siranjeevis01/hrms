using HRMS.Services.Payroll.Application.DTOs;
using MediatR;

namespace HRMS.Services.Payroll.Application.Queries.GetSalaryComponents;

public class GetSalaryComponentsQuery : IRequest<List<SalaryComponentDefDto>>
{
    public Guid TenantId { get; set; }
    public bool? ActiveOnly { get; set; }
}
