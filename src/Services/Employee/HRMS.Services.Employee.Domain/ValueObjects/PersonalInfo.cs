using HRMS.Services.Employee.Domain.Enums;

namespace HRMS.Services.Employee.Domain.ValueObjects;

public class PersonalInfo
{
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public MaritalStatus? MaritalStatus { get; set; }
    public string? Nationality { get; set; }
    public string? BloodGroup { get; set; }
    public string? Religion { get; set; }
}
