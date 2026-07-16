using HRMS.Services.Employee.Application.Events;
using HRMS.Services.Employee.Application.Interfaces;
using HRMS.Services.Employee.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
{
    private readonly IEmployeeDbContext _context;

    public CreateEmployeeCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employeeCode = await GenerateEmployeeCode(request.CompanyId, cancellationToken);

        var employee = Domain.Entities.Employee.Create(
            employeeCode,
            request.UserId,
            request.CompanyId,
            request.BranchId,
            request.DepartmentId,
            request.DesignationId,
            request.GradeId,
            request.ReportsToId,
            request.FirstName,
            request.LastName,
            request.MiddleName,
            request.PreferredName,
            request.Email,
            request.PersonalEmail,
            request.PhoneNumber,
            request.DateOfBirth,
            request.Gender,
            request.MaritalStatus,
            request.Nationality,
            request.BloodGroup,
            request.ProfilePictureUrl,
            request.JoiningDate,
            request.EmploymentType,
            request.TenantId);

        employee.AddDomainEvent(new EmployeeCreatedEvent(employee.Id, employee.EmployeeCode, employee.Email));

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }

    private async Task<string> GenerateEmployeeCode(Guid companyId, CancellationToken cancellationToken)
    {
        var prefix = companyId.ToString()[..4].ToUpper();
        var count = await _context.Employees
            .CountAsync(e => e.CompanyId == companyId, cancellationToken);
        return $"{prefix}{(count + 1):D5}";
    }
}
