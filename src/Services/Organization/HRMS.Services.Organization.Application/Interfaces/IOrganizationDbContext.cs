using Microsoft.EntityFrameworkCore;
using HRMS.Services.Organization.Domain.Entities;

namespace HRMS.Services.Organization.Application.Interfaces;

public interface IOrganizationDbContext
{
    DbSet<Company> Companies { get; }
    DbSet<Branch> Branches { get; }
    DbSet<Department> Departments { get; }
    DbSet<Designation> Designations { get; }
    DbSet<Grade> Grades { get; }
    DbSet<Shift> Shifts { get; }
    DbSet<Holiday> Holidays { get; }
    DbSet<CompanyPolicy> CompanyPolicies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
