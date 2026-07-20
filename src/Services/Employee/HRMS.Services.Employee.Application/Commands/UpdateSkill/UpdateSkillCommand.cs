using HRMS.Services.Employee.Domain.Enums;
using MediatR;

namespace HRMS.Services.Employee.Application.Commands.UpdateSkill;

public class UpdateSkillCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public ProficiencyLevel? Proficiency { get; set; }
    public int? YearsOfExperience { get; set; }
    public DateTime? LastUsedDate { get; set; }
    public bool? IsEndorsed { get; set; }
    public string? EndorsedBy { get; set; }
}
