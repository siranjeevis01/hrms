using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Application.DTOs;

public class LessonDto : BaseDto
{
    public Guid ModuleId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string? ContentUrl { get; set; }
    public string? ContentText { get; set; }
    public int Duration { get; set; }
    public int Order { get; set; }
    public bool IsPreview { get; set; }
}
