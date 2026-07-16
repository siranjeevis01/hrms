namespace HRMS.Services.ProjectTask.Application.DTOs;

public class TimeLogDto
{
    public Guid Id { get; set; }
    public Guid? TaskItemId { get; set; }
    public Guid? StoryId { get; set; }
    public Guid EmployeeId { get; set; }
    public decimal Hours { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
