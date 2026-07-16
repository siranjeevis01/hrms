using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Domain.Enums;

namespace HRMS.Services.Helpdesk.Infrastructure.Repositories.Interfaces;

public interface IHelpdeskRepository
{
    Task<List<HelpdeskTicketDto>> GetPagedWithFiltersAsync(
        int pageNumber, int pageSize, Guid? employeeId,
        TicketStatus? status, TicketPriority? priority,
        TicketCategoryType? category, Guid? assignedTo,
        string? searchTerm, CancellationToken cancellationToken = default);
}
