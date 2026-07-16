namespace HRMS.Services.Identity.Application.DTOs;

public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public IReadOnlyList<string> Permissions { get; set; } = Array.Empty<string>();
}
