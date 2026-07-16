using HRMS.Services.Employee.Domain.Enums;

namespace HRMS.Services.Employee.Domain.Entities;

public class Dependent : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Relationship { get; private set; } = string.Empty;
    public DateTime? DateOfBirth { get; private set; }
    public Gender? Gender { get; private set; }
    public bool IsInsuranceBeneficiary { get; private set; }
    public string? PhoneNumber { get; private set; }

    private Dependent() { }

    public static Dependent Create(
        Guid employeeId, string name, string relationship, DateTime? dateOfBirth,
        Gender? gender, bool isInsuranceBeneficiary, string? phoneNumber)
    {
        return new Dependent
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            Name = name,
            Relationship = relationship,
            DateOfBirth = dateOfBirth,
            Gender = gender,
            IsInsuranceBeneficiary = isInsuranceBeneficiary,
            PhoneNumber = phoneNumber,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? name, string? relationship, DateTime? dateOfBirth,
        Gender? gender, bool? isInsuranceBeneficiary, string? phoneNumber)
    {
        Name = name ?? Name;
        Relationship = relationship ?? Relationship;
        if (dateOfBirth.HasValue) DateOfBirth = dateOfBirth;
        if (gender.HasValue) Gender = gender;
        if (isInsuranceBeneficiary.HasValue) IsInsuranceBeneficiary = isInsuranceBeneficiary.Value;
        PhoneNumber = phoneNumber ?? PhoneNumber;
        UpdatedAt = DateTime.UtcNow;
    }
}
