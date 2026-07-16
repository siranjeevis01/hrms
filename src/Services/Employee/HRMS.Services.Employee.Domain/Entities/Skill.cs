using HRMS.Services.Employee.Domain.Enums;

namespace HRMS.Services.Employee.Domain.Entities;

public class Skill : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Category { get; private set; }
    public ProficiencyLevel Proficiency { get; private set; }
    public int? YearsOfExperience { get; private set; }
    public DateTime? LastUsedDate { get; private set; }
    public bool IsEndorsed { get; private set; }
    public string? EndorsedBy { get; private set; }

    private Skill() { }

    public static Skill Create(
        Guid employeeId, string name, string? category, ProficiencyLevel proficiency,
        int? yearsOfExperience, DateTime? lastUsedDate, bool isEndorsed, string? endorsedBy)
    {
        return new Skill
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            Name = name,
            Category = category,
            Proficiency = proficiency,
            YearsOfExperience = yearsOfExperience,
            LastUsedDate = lastUsedDate,
            IsEndorsed = isEndorsed,
            EndorsedBy = endorsedBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? name, string? category, ProficiencyLevel? proficiency,
        int? yearsOfExperience, DateTime? lastUsedDate, bool? isEndorsed, string? endorsedBy)
    {
        Name = name ?? Name;
        Category = category ?? Category;
        if (proficiency.HasValue) Proficiency = proficiency.Value;
        if (yearsOfExperience.HasValue) YearsOfExperience = yearsOfExperience;
        if (lastUsedDate.HasValue) LastUsedDate = lastUsedDate;
        if (isEndorsed.HasValue) IsEndorsed = isEndorsed.Value;
        EndorsedBy = endorsedBy ?? EndorsedBy;
        UpdatedAt = DateTime.UtcNow;
    }
}
