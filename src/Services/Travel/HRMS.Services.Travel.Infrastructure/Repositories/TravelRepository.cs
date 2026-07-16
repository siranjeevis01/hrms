using HRMS.Services.Travel.Application.DTOs;
using HRMS.Services.Travel.Application.Interfaces;
using HRMS.Services.Travel.Domain.Enums;
using HRMS.Services.Travel.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Infrastructure.Repositories;

public class TravelRepository : ITravelRepository
{
    private readonly ITravelDbContext _context;

    public TravelRepository(ITravelDbContext context)
    {
        _context = context;
    }

    public async Task<List<TravelRequestDto>> GetPagedWithFiltersAsync(
        int pageNumber, int pageSize, Guid? employeeId,
        TravelRequestStatus? status, string? destination,
        DateTime? fromDate, DateTime? toDate, string? searchTerm,
        CancellationToken cancellationToken = default)
    {
        var query = _context.TravelRequests.AsQueryable();

        if (employeeId.HasValue)
            query = query.Where(t => t.EmployeeId == employeeId.Value);

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        if (!string.IsNullOrWhiteSpace(destination))
            query = query.Where(t => t.Destination.Contains(destination));

        if (fromDate.HasValue)
            query = query.Where(t => t.StartDate >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(t => t.EndDate <= toDate.Value);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var search = searchTerm.ToLower();
            query = query.Where(t =>
                t.Purpose.ToLower().Contains(search) ||
                t.Destination.ToLower().Contains(search));
        }

        return await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TravelRequestDto
            {
                Id = t.Id,
                EmployeeId = t.EmployeeId,
                Purpose = t.Purpose,
                Destination = t.Destination,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                Status = t.Status,
                EstimatedCost = t.EstimatedCost,
                ActualCost = t.ActualCost,
                Currency = t.Currency,
                TransportMode = t.TransportMode,
                AccommodationType = t.AccommodationType,
                TenantId = t.TenantId,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
