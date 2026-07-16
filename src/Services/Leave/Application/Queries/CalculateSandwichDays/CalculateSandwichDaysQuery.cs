using MediatR;

namespace HRMS.Services.Leave.Application.Queries.CalculateSandwichDays;

public class CalculateSandwichDaysQuery : IRequest<decimal>
{
    public Guid EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? TenantId { get; set; }
}
