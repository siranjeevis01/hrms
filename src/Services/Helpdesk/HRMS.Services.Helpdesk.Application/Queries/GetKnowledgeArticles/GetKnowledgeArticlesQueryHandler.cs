using AutoMapper;
using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Queries.GetKnowledgeArticles;

public class GetKnowledgeArticlesQueryHandler : IRequestHandler<GetKnowledgeArticlesQuery, PagedResult<KnowledgeArticleDto>>
{
    private readonly IHelpdeskDbContext _context;
    private readonly IMapper _mapper;

    public GetKnowledgeArticlesQueryHandler(IHelpdeskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<KnowledgeArticleDto>> Handle(GetKnowledgeArticlesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.KnowledgeArticles
            .Where(a => a.TenantId == request.TenantId);

        if (request.CategoryId.HasValue)
            query = query.Where(a => a.CategoryId == request.CategoryId.Value);

        if (request.IsPublished.HasValue)
            query = query.Where(a => a.IsPublished == request.IsPublished.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.ToLower();
            query = query.Where(a =>
                a.Title.ToLower().Contains(search) ||
                a.Content.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var articles = await query
            .OrderByDescending(a => a.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<KnowledgeArticleDto>>(articles);

        return new PagedResult<KnowledgeArticleDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
