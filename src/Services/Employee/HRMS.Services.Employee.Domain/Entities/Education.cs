namespace HRMS.Services.Employee.Domain.Entities;

public class Education : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public string Institution { get; private set; } = string.Empty;
    public string Degree { get; private set; } = string.Empty;
    public string? FieldOfStudy { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public string? Grade { get; private set; }
    public decimal? Percentage { get; private set; }
    public bool IsHighest { get; private set; }
    public string? Country { get; private set; }
    public string? University { get; private set; }

    private Education() { }

    public static Education Create(
        Guid employeeId, string institution, string degree, string? fieldOfStudy,
        DateTime? startDate, DateTime? endDate, string? grade, decimal? percentage,
        bool isHighest, string? country, string? university)
    {
        return new Education
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            Institution = institution,
            Degree = degree,
            FieldOfStudy = fieldOfStudy,
            StartDate = startDate,
            EndDate = endDate,
            Grade = grade,
            Percentage = percentage,
            IsHighest = isHighest,
            Country = country,
            University = university,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? institution, string? degree, string? fieldOfStudy,
        DateTime? startDate, DateTime? endDate, string? grade, decimal? percentage,
        bool? isHighest, string? country, string? university)
    {
        Institution = institution ?? Institution;
        Degree = degree ?? Degree;
        FieldOfStudy = fieldOfStudy ?? FieldOfStudy;
        if (startDate.HasValue) StartDate = startDate;
        if (endDate.HasValue) EndDate = endDate;
        Grade = grade ?? Grade;
        if (percentage.HasValue) Percentage = percentage;
        if (isHighest.HasValue) IsHighest = isHighest.Value;
        Country = country ?? Country;
        University = university ?? University;
        UpdatedAt = DateTime.UtcNow;
    }
}
