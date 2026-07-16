using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Application.Interfaces;
using HRMS.Services.Helpdesk.Domain.Enums;
using HRMS.Services.Helpdesk.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Infrastructure.Repositories;

public class HelpdeskRepository : IHelpdeskRepository
{
    private readonly IHelpdeskDbContext _context;

    public HelpdeskRepository(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task<List<HelpdeskTicketDto>> GetPagedWithFiltersAsync(
        int pageNumber, int pageSize, Guid? employeeId,
        TicketStatus? status, TicketPriority? priority,
        TicketCategoryType? category, Guid? assignedTo,
        string? searchTerm, CancellationToken cancellationToken = default)
    {
        var query = _context.HelpdeskTickets.AsQueryable();

        if (employeeId.HasValue)
            query = query.Where(t => t.EmployeeId == employeeId.Value);

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        if (priority.HasValue)
            query = query.Where(t => t.Priority == priority.Value);

        if (category.HasValue)
            query = query.Where(t => t.Category == category.Value);

        if (assignedTo.HasValue)
            query = query.Where(t => t.AssignedTo == assignedTo.Value);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var search = searchTerm.ToLower();
            query = query.Where(t =>
                t.Subject.ToLower().Contains(search) ||
                t.Description.ToLower().Contains(search));
        }

        return await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new HelpdeskTicketDto
            {
                Id = t.Id,
                EmployeeId = t.EmployeeId,
                Subject = t.Subject,
                Description = t.Description,
                Category = t.Category,
                Priority = t.Priority,
                Status = t.Status,
                AssignedTo = t.AssignedTo,
                DepartmentId = t.DepartmentId,
                ResolutionNotes = t.ResolutionNotes,
                ResolvedAt = t.ResolvedAt,
                TenantId = t.TenantId,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
