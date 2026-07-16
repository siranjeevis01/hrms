using HRMS.Services.Helpdesk.Application.DTOs;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Queries.GetFaqs;

public class GetFaqsQuery : IRequest<List<FaqDto>>
{
    public Guid? CategoryId { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
