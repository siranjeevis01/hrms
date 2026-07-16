using AutoMapper;
using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Queries.GetKnowledgeArticle;

public class GetKnowledgeArticleQueryHandler : IRequestHandler<GetKnowledgeArticleQuery, KnowledgeArticleDto?>
{
    private readonly IHelpdeskDbContext _context;
    private readonly IMapper _mapper;

    public GetKnowledgeArticleQueryHandler(IHelpdeskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<KnowledgeArticleDto?> Handle(GetKnowledgeArticleQuery request, CancellationToken cancellationToken)
    {
        var article = await _context.KnowledgeArticles
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (article == null)
            return null;

        article.IncrementViewCount();
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<KnowledgeArticleDto>(article);
    }
}
