using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.UpdateComment;

public class UpdateCommentCommand : IRequest
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
}
