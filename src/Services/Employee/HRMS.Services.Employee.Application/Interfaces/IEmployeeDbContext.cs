using HRMS.Services.Employee.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Interfaces;

public interface IEmployeeDbContext
{
    DbSet<Domain.Entities.Employee> Employees { get; }
    DbSet<EmergencyContact> EmergencyContacts { get; }
    DbSet<EmployeeDocument> Documents { get; }
    DbSet<BankDetail> BankDetails { get; }
    DbSet<Education> Educations { get; }
    DbSet<WorkExperience> WorkExperiences { get; }
    DbSet<Certification> Certifications { get; }
    DbSet<Skill> Skills { get; }
    DbSet<SalaryStructure> SalaryStructures { get; }
    DbSet<EmployeeHistory> EmployeeHistories { get; }
    DbSet<Dependent> Dependents { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
