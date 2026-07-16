namespace HRMS.Services.Expense.Application.DTOs;

public class ExpenseCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? DefaultPolicyId { get; set; }
    public bool IsActive { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
