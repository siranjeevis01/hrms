using HRMS.Services.Employee.Domain.Enums;

namespace HRMS.Services.Employee.Application.DTOs;

public class SkillDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Category { get; set; }
    public ProficiencyLevel Proficiency { get; set; }
    public int? YearsOfExperience { get; set; }
    public DateTime? LastUsedDate { get; set; }
    public bool IsEndorsed { get; set; }
    public string? EndorsedBy { get; set; }
}
