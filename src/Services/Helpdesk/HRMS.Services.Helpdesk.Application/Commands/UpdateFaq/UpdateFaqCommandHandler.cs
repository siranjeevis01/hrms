using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Commands.UpdateFaq;

public class UpdateFaqCommandHandler : IRequestHandler<UpdateFaqCommand>
{
    private readonly IHelpdeskDbContext _context;

    public UpdateFaqCommandHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateFaqCommand request, CancellationToken cancellationToken)
    {
        var faq = await _context.Faqs
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (faq == null)
            throw new InvalidOperationException($"FAQ with ID {request.Id} not found.");

        faq.Update(request.Question, request.Answer, request.CategoryId, request.Order, request.IsActive);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
