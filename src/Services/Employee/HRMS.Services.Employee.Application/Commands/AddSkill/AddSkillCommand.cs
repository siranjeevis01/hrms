using HRMS.Services.Employee.Domain.Enums;
using MediatR;

namespace HRMS.Services.Employee.Application.Commands.AddSkill;

public class AddSkillCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Category { get; set; }
    public ProficiencyLevel Proficiency { get; set; }
    public int? YearsOfExperience { get; set; }
    public DateTime? LastUsedDate { get; set; }
    public bool IsEndorsed { get; set; }
    public string? EndorsedBy { get; set; }
}
