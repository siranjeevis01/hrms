namespace HRMS.Services.Performance.Application.DTOs;

public class FeedbackAnswerDto
{
    public Guid Id { get; set; }
    public Guid Feedback360Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public decimal? Rating { get; set; }
    public string? Comments { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
