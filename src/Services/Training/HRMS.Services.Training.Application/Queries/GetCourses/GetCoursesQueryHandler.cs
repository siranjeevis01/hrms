using AutoMapper;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetCourses;

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, PagedResult<CourseDto>>
{
    private readonly ITrainingDbContext _context;
    private readonly IMapper _mapper;

    public GetCoursesQueryHandler(ITrainingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Courses
            .Include(c => c.Modules)
            .Include(c => c.Enrollments)
            .Where(c => !c.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = request.SearchTerm.ToLower();
            query = query.Where(c => c.Title.ToLower().Contains(term) || c.Code.ToLower().Contains(term) || (c.Description != null && c.Description.ToLower().Contains(term)));
        }

        if (!string.IsNullOrWhiteSpace(request.Category))
            query = query.Where(c => c.Category == request.Category);

        if (!string.IsNullOrWhiteSpace(request.Status))
            query = query.Where(c => c.Status.ToString() == request.Status);

        if (request.DepartmentId.HasValue)
            query = query.Where(c => c.DepartmentId == request.DepartmentId);

        var totalCount = await query.CountAsync(cancellationToken);

        var courses = await query
            .OrderByDescending(c => c.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var dtos = courses.Select(c =>
        {
            var dto = _mapper.Map<CourseDto>(c);
            dto.ModuleCount = c.Modules.Count;
            dto.EnrollmentCount = c.Enrollments.Count(e => !e.IsDeleted);
            return dto;
        }).ToList();

        return new PagedResult<CourseDto>(dtos, totalCount, request.PageNumber, request.PageSize);
    }
}
