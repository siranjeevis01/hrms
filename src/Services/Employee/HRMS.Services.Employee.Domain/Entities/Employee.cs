using HRMS.Services.Employee.Domain.Enums;
using MediatR;

namespace HRMS.Services.Employee.Domain.Entities;

public class Employee : AggregateRoot
{
    public string EmployeeCode { get; private set; } = string.Empty;
    public Guid UserId { get; private set; }
    public Guid CompanyId { get; private set; }
    public Guid? BranchId { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Guid DesignationId { get; private set; }
    public Guid? GradeId { get; private set; }
    public Guid? ReportsToId { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string? MiddleName { get; private set; }
    public string? PreferredName { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string? PersonalEmail { get; private set; }
    public string? PhoneNumber { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public Gender? Gender { get; private set; }
    public MaritalStatus? MaritalStatus { get; private set; }
    public string? Nationality { get; private set; }
    public string? BloodGroup { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public DateTime JoiningDate { get; private set; }
    public DateTime? ConfirmationDate { get; private set; }
    public DateTime? LastWorkingDate { get; private set; }
    public EmploymentType EmploymentType { get; private set; }
    public EmploymentStatus EmploymentStatus { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private readonly List<EmergencyContact> _emergencyContacts = new();
    public IReadOnlyCollection<EmergencyContact> EmergencyContacts => _emergencyContacts.AsReadOnly();

    private readonly List<EmployeeDocument> _documents = new();
    public IReadOnlyCollection<EmployeeDocument> Documents => _documents.AsReadOnly();

    private readonly List<BankDetail> _bankDetails = new();
    public IReadOnlyCollection<BankDetail> BankDetails => _bankDetails.AsReadOnly();

    private readonly List<Education> _educations = new();
    public IReadOnlyCollection<Education> Educations => _educations.AsReadOnly();

    private readonly List<WorkExperience> _workExperiences = new();
    public IReadOnlyCollection<WorkExperience> WorkExperiences => _workExperiences.AsReadOnly();

    private readonly List<Certification> _certifications = new();
    public IReadOnlyCollection<Certification> Certifications => _certifications.AsReadOnly();

    private readonly List<Skill> _skills = new();
    public IReadOnlyCollection<Skill> Skills => _skills.AsReadOnly();

    private readonly List<SalaryStructure> _salaryStructures = new();
    public IReadOnlyCollection<SalaryStructure> SalaryStructures => _salaryStructures.AsReadOnly();

    private readonly List<EmployeeHistory> _histories = new();
    public IReadOnlyCollection<EmployeeHistory> Histories => _histories.AsReadOnly();

    private readonly List<Dependent> _dependents = new();
    public IReadOnlyCollection<Dependent> Dependents => _dependents.AsReadOnly();

    private Employee() { }

    public static Employee Create(
        string employeeCode,
        Guid userId,
        Guid companyId,
        Guid? branchId,
        Guid departmentId,
        Guid designationId,
        Guid? gradeId,
        Guid? reportsToId,
        string firstName,
        string lastName,
        string? middleName,
        string? preferredName,
        string email,
        string? personalEmail,
        string? phoneNumber,
        DateTime? dateOfBirth,
        Gender? gender,
        MaritalStatus? maritalStatus,
        string? nationality,
        string? bloodGroup,
        string? profilePictureUrl,
        DateTime joiningDate,
        EmploymentType employmentType,
        string tenantId)
    {
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            EmployeeCode = employeeCode,
            UserId = userId,
            CompanyId = companyId,
            BranchId = branchId,
            DepartmentId = departmentId,
            DesignationId = designationId,
            GradeId = gradeId,
            ReportsToId = reportsToId,
            FirstName = firstName,
            LastName = lastName,
            MiddleName = middleName,
            PreferredName = preferredName,
            Email = email,
            PersonalEmail = personalEmail,
            PhoneNumber = phoneNumber,
            DateOfBirth = dateOfBirth,
            Gender = gender,
            MaritalStatus = maritalStatus,
            Nationality = nationality,
            BloodGroup = bloodGroup,
            ProfilePictureUrl = profilePictureUrl,
            JoiningDate = joiningDate,
            EmploymentType = employmentType,
            EmploymentStatus = EmploymentStatus.Active,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return employee;
    }

    public void Promote(Guid newDesignationId, Guid? newGradeId, string? reason)
    {
        var previousDesignationId = DesignationId;
        DesignationId = newDesignationId;
        if (newGradeId.HasValue)
            GradeId = newGradeId;
        UpdatedAt = DateTime.UtcNow;

        _histories.Add(EmployeeHistory.Create(
            Id, EmployeeAction.Promotion, DateTime.UtcNow,
            $"Designation: {previousDesignationId}", $"Designation: {newDesignationId}",
            reason, null, TenantId));
    }

    public void Transfer(Guid? newBranchId, Guid? newDepartmentId, Guid? newDesignationId, string? reason)
    {
        var prevValues = new List<string>();
        var newValues = new List<string>();

        if (newBranchId.HasValue)
        {
            prevValues.Add($"Branch: {BranchId}");
            BranchId = newBranchId;
            newValues.Add($"Branch: {newBranchId}");
        }
        if (newDepartmentId.HasValue)
        {
            prevValues.Add($"Department: {DepartmentId}");
            DepartmentId = newDepartmentId.Value;
            newValues.Add($"Department: {newDepartmentId}");
        }
        if (newDesignationId.HasValue)
        {
            prevValues.Add($"Designation: {DesignationId}");
            DesignationId = newDesignationId.Value;
            newValues.Add($"Designation: {newDesignationId}");
        }

        UpdatedAt = DateTime.UtcNow;

        _histories.Add(EmployeeHistory.Create(
            Id, EmployeeAction.Transfer, DateTime.UtcNow,
            string.Join(", ", prevValues), string.Join(", ", newValues),
            reason, null, TenantId));
    }

    public void Terminate(string? reason)
    {
        var previousStatus = EmploymentStatus;
        EmploymentStatus = EmploymentStatus.Terminated;
        LastWorkingDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        _histories.Add(EmployeeHistory.Create(
            Id, EmployeeAction.Termination, DateTime.UtcNow,
            previousStatus.ToString(), EmploymentStatus.Terminated.ToString(),
            reason, null, TenantId));
    }

    public void ChangeStatus(EmploymentStatus newStatus, string? reason)
    {
        var previousStatus = EmploymentStatus;
        EmploymentStatus = newStatus;
        UpdatedAt = DateTime.UtcNow;

        _histories.Add(EmployeeHistory.Create(
            Id, EmployeeAction.StatusChange, DateTime.UtcNow,
            previousStatus.ToString(), newStatus.ToString(),
            reason, null, TenantId));
    }

    public void UpdatePersonalInfo(
        string? personalEmail,
        string? phoneNumber,
        DateTime? dateOfBirth,
        Gender? gender,
        MaritalStatus? maritalStatus,
        string? nationality,
        string? bloodGroup,
        string? profilePictureUrl)
    {
        PersonalEmail = personalEmail ?? PersonalEmail;
        PhoneNumber = phoneNumber ?? PhoneNumber;
        DateOfBirth = dateOfBirth ?? DateOfBirth;
        Gender = gender ?? Gender;
        MaritalStatus = maritalStatus ?? MaritalStatus;
        Nationality = nationality ?? Nationality;
        BloodGroup = bloodGroup ?? BloodGroup;
        ProfilePictureUrl = profilePictureUrl ?? ProfilePictureUrl;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(
        Guid? branchId,
        Guid? departmentId,
        Guid? designationId,
        Guid? gradeId,
        Guid? reportsToId,
        string? firstName,
        string? lastName,
        string? middleName,
        string? preferredName,
        string? email,
        string? phoneNumber,
        EmploymentType? employmentType)
    {
        BranchId = branchId ?? BranchId;
        DepartmentId = departmentId ?? DepartmentId;
        DesignationId = designationId ?? DesignationId;
        GradeId = gradeId ?? GradeId;
        ReportsToId = reportsToId ?? ReportsToId;
        FirstName = firstName ?? FirstName;
        LastName = lastName ?? LastName;
        MiddleName = middleName ?? MiddleName;
        PreferredName = preferredName ?? PreferredName;
        Email = email ?? Email;
        PhoneNumber = phoneNumber ?? PhoneNumber;
        EmploymentType = employmentType ?? EmploymentType;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddDomainEvent(INotification domainEvent)
    {
        RaiseEvent(domainEvent);
    }
}
