using HRMS.Services.Payroll.Application.DTOs;
using MediatR;

namespace HRMS.Services.Payroll.Application.Queries.GetBonuses;

public class GetBonusesQuery : IRequest<List<BonusDto>>
{
    public Guid? EmployeeId { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }
}
