using HRMS.Services.Recruitment.Domain.Enums;

namespace HRMS.Services.Recruitment.Application.DTOs;

public class JobPostingDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public Guid DesignationId { get; set; }
    public Guid BranchId { get; set; }
    public string EmploymentType { get; set; } = string.Empty;
    public int MinExperience { get; set; }
    public int MaxExperience { get; set; }
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }
    public string Currency { get; set; } = "INR";
    public string Skills { get; set; } = "[]";
    public string Requirements { get; set; } = "[]";
    public string Responsibilities { get; set; } = "[]";
    public string Benefits { get; set; } = "[]";
    public JobStatus Status { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public int HeadCount { get; set; }
    public int FilledCount { get; set; }
    public Guid HiringManagerId { get; set; }
    public Guid RecruiterId { get; set; }
    public bool IsUrgent { get; set; }
    public DateTime? ApplicationDeadline { get; set; }
    public Guid TenantId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
