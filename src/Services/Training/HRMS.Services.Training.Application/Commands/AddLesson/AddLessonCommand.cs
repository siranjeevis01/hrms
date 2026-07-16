using HRMS.Services.Training.Domain.Enums;
using MediatR;

namespace HRMS.Services.Training.Application.Commands.AddLesson;

public class AddLessonCommand : IRequest<Guid>
{
    public Guid ModuleId { get; set; }
    public string Title { get; set; } = string.Empty;
    public ContentType ContentType { get; set; }
    public string? ContentUrl { get; set; }
    public string? ContentText { get; set; }
    public int Duration { get; set; }
    public int Order { get; set; }
    public bool IsPreview { get; set; }
    public Guid TenantId { get; set; }
}
