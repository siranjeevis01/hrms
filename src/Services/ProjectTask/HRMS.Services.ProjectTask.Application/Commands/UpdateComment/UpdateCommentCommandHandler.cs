using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.UpdateComment;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
{
    private readonly IProjectTaskDbContext _context;

    public UpdateCommentCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Comment with ID {request.Id} not found.");

        comment.UpdateContent(request.Content);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
