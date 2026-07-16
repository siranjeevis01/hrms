using AutoMapper;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Recruitment.Application.Queries.GetJobPostings;

public class GetJobPostingsQueryHandler : IRequestHandler<GetJobPostingsQuery, PagedResult<JobPostingDto>>
{
    private readonly IRecruitmentDbContext _context;
    private readonly IMapper _mapper;

    public GetJobPostingsQueryHandler(IRecruitmentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<JobPostingDto>> Handle(GetJobPostingsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.JobPostings.AsQueryable();

        if (request.Status.HasValue)
            query = query.Where(j => j.Status == request.Status.Value);

        if (request.DepartmentId.HasValue)
            query = query.Where(j => j.DepartmentId == request.DepartmentId.Value);

        if (request.TenantId.HasValue)
            query = query.Where(j => j.TenantId == request.TenantId.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.ToLower();
            query = query.Where(j =>
                j.Title.ToLower().Contains(search) ||
                j.Description.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var jobPostings = await query
            .OrderByDescending(j => j.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<JobPostingDto>>(jobPostings);

        return PagedResult<JobPostingDto>.Create(items, totalCount, request.PageNumber, request.PageSize);
    }
}
