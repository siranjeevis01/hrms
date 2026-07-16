namespace HRMS.Services.Employee.Domain.Entities;

public class EmergencyContact : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Relationship { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string? SecondaryPhone { get; private set; }
    public string? Email { get; private set; }
    public string? Address { get; private set; }
    public bool IsPrimary { get; private set; }

    private EmergencyContact() { }

    public static EmergencyContact Create(
        Guid employeeId, string name, string relationship, string phoneNumber,
        string? secondaryPhone, string? email, string? address, bool isPrimary)
    {
        return new EmergencyContact
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            Name = name,
            Relationship = relationship,
            PhoneNumber = phoneNumber,
            SecondaryPhone = secondaryPhone,
            Email = email,
            Address = address,
            IsPrimary = isPrimary,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? name, string? relationship, string? phoneNumber,
        string? secondaryPhone, string? email, string? address, bool? isPrimary)
    {
        Name = name ?? Name;
        Relationship = relationship ?? Relationship;
        PhoneNumber = phoneNumber ?? PhoneNumber;
        SecondaryPhone = secondaryPhone ?? SecondaryPhone;
        Email = email ?? Email;
        Address = address ?? Address;
        if (isPrimary.HasValue) IsPrimary = isPrimary.Value;
        UpdatedAt = DateTime.UtcNow;
    }
}
