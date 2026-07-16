using MediatR;

namespace HRMS.Services.Dashboard.Application.Commands.UpdateDashboard;

public class UpdateDashboardCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? IsDefault { get; set; }
    public bool? IsPublic { get; set; }
    public string? Layout { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
