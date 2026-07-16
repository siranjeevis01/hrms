using MediatR;

namespace HRMS.Services.Training.Application.Commands.UpdateLessonProgress;

public class UpdateLessonProgressCommand : IRequest
{
    public Guid EnrollmentId { get; set; }
    public Guid LessonId { get; set; }
    public string Status { get; set; } = string.Empty;
}
