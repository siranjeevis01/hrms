using MediatR;

namespace HRMS.Services.Training.Application.Commands.UnpublishCourse;

public class UnpublishCourseCommand : IRequest
{
    public Guid Id { get; set; }
}
