namespace HRMS.Services.Employee.Application.DTOs;

public class EducationDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string Institution { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string? FieldOfStudy { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Grade { get; set; }
    public decimal? Percentage { get; set; }
    public bool IsHighest { get; set; }
    public string? Country { get; set; }
    public string? University { get; set; }
}
