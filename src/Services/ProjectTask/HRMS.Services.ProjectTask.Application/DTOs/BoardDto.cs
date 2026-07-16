using HRMS.Services.ProjectTask.Domain.Enums;

namespace HRMS.Services.ProjectTask.Application.DTOs;

public class BoardDto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public BoardType Type { get; set; }
    public string? Columns { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
