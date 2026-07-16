using HRMS.Services.Helpdesk.Application.DTOs;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Queries.GetTicketCategories;

public class GetTicketCategoriesQuery : IRequest<List<TicketCategoryDto>>
{
    public string TenantId { get; set; } = string.Empty;
}
