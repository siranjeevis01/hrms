using MediatR;

namespace HRMS.Services.Employee.Application.Commands.CreateSalaryStructure;

public class CreateSalaryStructureCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal CTC { get; set; }
    public string Currency { get; set; } = "INR";
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
    public string? ComponentDetails { get; set; }
}
