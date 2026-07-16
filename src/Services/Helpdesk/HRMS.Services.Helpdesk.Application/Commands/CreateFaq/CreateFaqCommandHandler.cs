using HRMS.Services.Helpdesk.Application.Interfaces;
using HRMS.Services.Helpdesk.Domain.Entities;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.CreateFaq;

public class CreateFaqCommandHandler : IRequestHandler<CreateFaqCommand, Guid>
{
    private readonly IHelpdeskDbContext _context;

    public CreateFaqCommandHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateFaqCommand request, CancellationToken cancellationToken)
    {
        var faq = Faq.Create(
            request.Question,
            request.Answer,
            request.CategoryId,
            request.Order,
            request.TenantId);

        _context.Faqs.Add(faq);
        await _context.SaveChangesAsync(cancellationToken);

        return faq.Id;
    }
}
