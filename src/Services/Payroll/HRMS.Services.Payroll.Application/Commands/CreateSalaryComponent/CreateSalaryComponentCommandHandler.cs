using HRMS.Services.Payroll.Application.Interfaces;
using HRMS.Services.Payroll.Domain.Entities;
using MediatR;

namespace HRMS.Services.Payroll.Application.Commands.CreateSalaryComponent;

public class CreateSalaryComponentCommandHandler : IRequestHandler<CreateSalaryComponentCommand, Guid>
{
    private readonly IPayrollDbContext _context;

    public CreateSalaryComponentCommandHandler(IPayrollDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateSalaryComponentCommand request, CancellationToken cancellationToken)
    {
        var component = new SalaryComponentDef(
            request.Name, request.Code, request.Type, request.CalculationType,
            request.DefaultValue, request.IsTaxable, request.IsPensionable,
            request.IsPFApplicable, request.IsESIApplicable, request.SortOrder, request.TenantId);

        _context.SalaryComponentDefs.Add(component);
        await _context.SaveChangesAsync(cancellationToken);

        return component.Id;
    }
}
