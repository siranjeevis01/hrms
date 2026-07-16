using HRMS.Services.Training.Domain.Enums;
using MediatR;

namespace HRMS.Services.Training.Application.Commands.UpdateLesson;

public class UpdateLessonCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public ContentType? ContentType { get; set; }
    public string? ContentUrl { get; set; }
    public string? ContentText { get; set; }
    public int? Duration { get; set; }
    public int? Order { get; set; }
    public bool? IsPreview { get; set; }
}
