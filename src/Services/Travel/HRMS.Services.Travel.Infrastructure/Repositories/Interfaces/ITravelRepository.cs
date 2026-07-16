using HRMS.Services.Travel.Application.DTOs;
using HRMS.Services.Travel.Domain.Enums;

namespace HRMS.Services.Travel.Infrastructure.Repositories.Interfaces;

public interface ITravelRepository
{
    Task<List<TravelRequestDto>> GetPagedWithFiltersAsync(
        int pageNumber, int pageSize, Guid? employeeId,
        TravelRequestStatus? status, string? destination,
        DateTime? fromDate, DateTime? toDate, string? searchTerm,
        CancellationToken cancellationToken = default);
}
