using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Application.Interfaces;
using HRMS.Services.Employee.Domain.Enums;
using HRMS.Services.Employee.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IEmployeeDbContext _context;

    public EmployeeRepository(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Employee?> GetByEmployeeCodeAsync(string employeeCode, CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(e => e.EmployeeCode == employeeCode, cancellationToken);
    }

    public async Task<Domain.Entities.Employee?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(e => e.UserId == userId, cancellationToken);
    }

    public async Task<List<Domain.Entities.Employee>> GetByDepartmentAsync(Guid departmentId, CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .Where(e => e.DepartmentId == departmentId)
            .OrderBy(e => e.LastName)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Domain.Entities.Employee>> GetByDesignationAsync(Guid designationId, CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .Where(e => e.DesignationId == designationId)
            .OrderBy(e => e.LastName)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Domain.Entities.Employee>> GetByStatusAsync(EmploymentStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .Where(e => e.EmploymentStatus == status)
            .OrderBy(e => e.LastName)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<EmployeeListDto>> GetPagedWithFiltersAsync(
        int pageNumber, int pageSize, Guid? departmentId, Guid? designationId,
        EmploymentStatus? status, string? searchTerm, CancellationToken cancellationToken = default)
    {
        var query = _context.Employees.AsQueryable();

        if (departmentId.HasValue)
            query = query.Where(e => e.DepartmentId == departmentId.Value);

        if (designationId.HasValue)
            query = query.Where(e => e.DesignationId == designationId.Value);

        if (status.HasValue)
            query = query.Where(e => e.EmploymentStatus == status.Value);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var search = searchTerm.ToLower();
            query = query.Where(e =>
                e.FirstName.ToLower().Contains(search) ||
                e.LastName.ToLower().Contains(search) ||
                e.EmployeeCode.ToLower().Contains(search) ||
                e.Email.ToLower().Contains(search));
        }

        return await query
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new EmployeeListDto
            {
                Id = e.Id,
                EmployeeCode = e.EmployeeCode,
                FirstName = e.FirstName,
                LastName = e.LastName,
                PreferredName = e.PreferredName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                DepartmentId = e.DepartmentId,
                DesignationId = e.DesignationId,
                EmploymentStatus = e.EmploymentStatus,
                EmploymentType = e.EmploymentType,
                JoiningDate = e.JoiningDate,
                ProfilePictureUrl = e.ProfilePictureUrl
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<EmployeeDto?> GetEmployeeFullProfileAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        var employee = await _context.Employees
            .Include(e => e.EmergencyContacts)
            .Include(e => e.Documents)
            .Include(e => e.BankDetails)
            .Include(e => e.Educations)
            .Include(e => e.WorkExperiences)
            .Include(e => e.Certifications)
            .Include(e => e.Skills)
            .Include(e => e.SalaryStructures)
            .Include(e => e.Dependents)
            .AsSplitQuery()
            .FirstOrDefaultAsync(e => e.Id == employeeId, cancellationToken);

        if (employee == null)
            return null;

        return new EmployeeDto
        {
            Id = employee.Id,
            EmployeeCode = employee.EmployeeCode,
            UserId = employee.UserId,
            CompanyId = employee.CompanyId,
            BranchId = employee.BranchId,
            DepartmentId = employee.DepartmentId,
            DesignationId = employee.DesignationId,
            GradeId = employee.GradeId,
            ReportsToId = employee.ReportsToId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            MiddleName = employee.MiddleName,
            PreferredName = employee.PreferredName,
            Email = employee.Email,
            PersonalEmail = employee.PersonalEmail,
            PhoneNumber = employee.PhoneNumber,
            DateOfBirth = employee.DateOfBirth,
            Gender = employee.Gender,
            MaritalStatus = employee.MaritalStatus,
            Nationality = employee.Nationality,
            BloodGroup = employee.BloodGroup,
            ProfilePictureUrl = employee.ProfilePictureUrl,
            JoiningDate = employee.JoiningDate,
            ConfirmationDate = employee.ConfirmationDate,
            LastWorkingDate = employee.LastWorkingDate,
            EmploymentType = employee.EmploymentType,
            EmploymentStatus = employee.EmploymentStatus,
            TenantId = employee.TenantId,
            EmergencyContacts = employee.EmergencyContacts.Select(c => new EmergencyContactDto
            {
                Id = c.Id,
                EmployeeId = c.EmployeeId,
                Name = c.Name,
                Relationship = c.Relationship,
                PhoneNumber = c.PhoneNumber,
                SecondaryPhone = c.SecondaryPhone,
                Email = c.Email,
                Address = c.Address,
                IsPrimary = c.IsPrimary
            }).ToList(),
            Documents = employee.Documents.Select(d => new EmployeeDocumentDto
            {
                Id = d.Id,
                EmployeeId = d.EmployeeId,
                DocumentType = d.DocumentType,
                FileName = d.FileName,
                FileUrl = d.FileUrl,
                FileSize = d.FileSize,
                MimeType = d.MimeType,
                ExpiryDate = d.ExpiryDate,
                IsVerified = d.IsVerified,
                VerifiedBy = d.VerifiedBy,
                VerifiedAt = d.VerifiedAt
            }).ToList(),
            BankDetails = employee.BankDetails.Select(b => new BankDetailDto
            {
                Id = b.Id,
                EmployeeId = b.EmployeeId,
                BankName = b.BankName,
                BankCode = b.BankCode,
                AccountNumber = b.AccountNumber,
                AccountHolderName = b.AccountHolderName,
                IsPrimary = b.IsPrimary,
                TaxJurisdiction = b.TaxJurisdiction,
                Currency = b.Currency
            }).ToList(),
            Educations = employee.Educations.Select(e => new EducationDto
            {
                Id = e.Id,
                EmployeeId = e.EmployeeId,
                Institution = e.Institution,
                Degree = e.Degree,
                FieldOfStudy = e.FieldOfStudy,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Grade = e.Grade,
                Percentage = e.Percentage,
                IsHighest = e.IsHighest,
                Country = e.Country,
                University = e.University
            }).ToList(),
            WorkExperiences = employee.WorkExperiences.Select(w => new WorkExperienceDto
            {
                Id = w.Id,
                EmployeeId = w.EmployeeId,
                CompanyName = w.CompanyName,
                Designation = w.Designation,
                StartDate = w.StartDate,
                EndDate = w.EndDate,
                Description = w.Description,
                IsCurrent = w.IsCurrent,
                ReasonForLeaving = w.ReasonForLeaving,
                ReferenceName = w.ReferenceName,
                ReferencePhone = w.ReferencePhone
            }).ToList(),
            Certifications = employee.Certifications.Select(c => new CertificationDto
            {
                Id = c.Id,
                EmployeeId = c.EmployeeId,
                Name = c.Name,
                IssuingOrganization = c.IssuingOrganization,
                IssueDate = c.IssueDate,
                ExpiryDate = c.ExpiryDate,
                CredentialId = c.CredentialId,
                CredentialUrl = c.CredentialUrl,
                DoesNotExpire = c.DoesNotExpire
            }).ToList(),
            Skills = employee.Skills.Select(s => new SkillDto
            {
                Id = s.Id,
                EmployeeId = s.EmployeeId,
                Name = s.Name,
                Category = s.Category,
                Proficiency = s.Proficiency,
                YearsOfExperience = s.YearsOfExperience,
                LastUsedDate = s.LastUsedDate,
                IsEndorsed = s.IsEndorsed,
                EndorsedBy = s.EndorsedBy
            }).ToList(),
            SalaryStructures = employee.SalaryStructures.Select(s => new SalaryStructureDto
            {
                Id = s.Id,
                EmployeeId = s.EmployeeId,
                BasicSalary = s.BasicSalary,
                GrossSalary = s.GrossSalary,
                CTC = s.CTC,
                Currency = s.Currency,
                EffectiveFrom = s.EffectiveFrom,
                EffectiveTo = s.EffectiveTo,
                IsCurrent = s.IsCurrent,
                ComponentDetails = s.ComponentDetails
            }).ToList(),
            Dependents = employee.Dependents.Select(d => new DependentDto
            {
                Id = d.Id,
                EmployeeId = d.EmployeeId,
                Name = d.Name,
                Relationship = d.Relationship,
                DateOfBirth = d.DateOfBirth,
                Gender = d.Gender,
                IsInsuranceBeneficiary = d.IsInsuranceBeneficiary,
                PhoneNumber = d.PhoneNumber
            }).ToList()
        };
    }

    public async Task<List<EmployeeListDto>> GetNewHiresAsync(DateTime fromDate, DateTime toDate, Guid? departmentId, CancellationToken cancellationToken = default)
    {
        var query = _context.Employees
            .Where(e => e.JoiningDate >= fromDate && e.JoiningDate <= toDate);

        if (departmentId.HasValue)
            query = query.Where(e => e.DepartmentId == departmentId.Value);

        return await query
            .OrderBy(e => e.JoiningDate)
            .Select(e => new EmployeeListDto
            {
                Id = e.Id,
                EmployeeCode = e.EmployeeCode,
                FirstName = e.FirstName,
                LastName = e.LastName,
                PreferredName = e.PreferredName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                DepartmentId = e.DepartmentId,
                DesignationId = e.DesignationId,
                EmploymentStatus = e.EmploymentStatus,
                EmploymentType = e.EmploymentType,
                JoiningDate = e.JoiningDate,
                ProfilePictureUrl = e.ProfilePictureUrl
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<List<EmployeeListDto>> GetEmployeesLeavingAsync(Guid? departmentId, CancellationToken cancellationToken = default)
    {
        var query = _context.Employees
            .Where(e => e.EmploymentStatus == EmploymentStatus.OnNotice ||
                        e.EmploymentStatus == EmploymentStatus.Resigned ||
                        e.EmploymentStatus == EmploymentStatus.Terminated);

        if (departmentId.HasValue)
            query = query.Where(e => e.DepartmentId == departmentId.Value);

        return await query
            .OrderBy(e => e.LastWorkingDate)
            .Select(e => new EmployeeListDto
            {
                Id = e.Id,
                EmployeeCode = e.EmployeeCode,
                FirstName = e.FirstName,
                LastName = e.LastName,
                PreferredName = e.PreferredName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                DepartmentId = e.DepartmentId,
                DesignationId = e.DesignationId,
                EmploymentStatus = e.EmploymentStatus,
                EmploymentType = e.EmploymentType,
                JoiningDate = e.JoiningDate,
                ProfilePictureUrl = e.ProfilePictureUrl
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<List<DepartmentHeadCountDto>> GetDepartmentHeadCountAsync(Guid? companyId, CancellationToken cancellationToken = default)
    {
        var query = _context.Employees.AsQueryable();

        if (companyId.HasValue)
            query = query.Where(e => e.CompanyId == companyId.Value);

        return await query
            .GroupBy(e => e.DepartmentId)
            .Select(g => new DepartmentHeadCountDto
            {
                DepartmentId = g.Key,
                HeadCount = g.Count(),
                ActiveCount = g.Count(e => e.EmploymentStatus == EmploymentStatus.Active),
                OnNoticeCount = g.Count(e => e.EmploymentStatus == EmploymentStatus.OnNotice)
            })
            .OrderByDescending(d => d.HeadCount)
            .ToListAsync(cancellationToken);
    }

    public async Task<string> GenerateNextEmployeeCodeAsync(Guid companyId, CancellationToken cancellationToken = default)
    {
        var prefix = companyId.ToString()[..4].ToUpper();
        var count = await _context.Employees
            .CountAsync(e => e.CompanyId == companyId, cancellationToken);
        return $"{prefix}{(count + 1):D5}";
    }
}
