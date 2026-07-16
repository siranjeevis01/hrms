using HRMS.Services.ProjectTask.Application.Interfaces;
using HRMS.Services.ProjectTask.Domain.Entities;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateComment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
{
    private readonly IProjectTaskDbContext _context;

    public CreateCommentCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = Comment.Create(
            request.TaskItemId,
            request.StoryId,
            request.BugId,
            request.EmployeeId,
            request.Content,
            request.ParentCommentId,
            request.TenantId);

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync(cancellationToken);

        return comment.Id;
    }
}
