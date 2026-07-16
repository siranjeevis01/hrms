using MediatR;

namespace HRMS.Services.Dashboard.Application.Commands.CreateDashboard;

public class CreateDashboardCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public bool IsDefault { get; set; }
    public bool IsPublic { get; set; }
    public string? Layout { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
