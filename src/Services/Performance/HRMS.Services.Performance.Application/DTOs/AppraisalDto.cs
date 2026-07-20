using HRMS.Services.Performance.Domain.Enums;

namespace HRMS.Services.Performance.Application.DTOs;

public class AppraisalDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid? ManagerId { get; set; }
    public string Period { get; set; } = string.Empty;
    public AppraisalType Type { get; set; }
    public AppraisalStatus Status { get; set; }
    public decimal? FinalRating { get; set; }
    public decimal? HikePercentage { get; set; }
    public bool PromotionRecommended { get; set; }
    public decimal? Bonus { get; set; }
    public string? Comments { get; set; }
    public decimal? SelfRating { get; set; }
    public string? Achievements { get; set; }
    public string? Goals { get; set; }
    public string? Strengths { get; set; }
    public string? Improvements { get; set; }
    public Guid? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
