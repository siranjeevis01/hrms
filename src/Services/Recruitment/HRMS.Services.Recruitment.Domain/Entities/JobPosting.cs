using HRMS.Shared.Kernel.Common;
using HRMS.Services.Recruitment.Domain.Enums;

namespace HRMS.Services.Recruitment.Domain.Entities;

public class JobPosting : AggregateRoot
{
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Guid DepartmentId { get; private set; }
    public Guid DesignationId { get; private set; }
    public Guid BranchId { get; private set; }
    public EmploymentType EmploymentType { get; private set; }
    public int MinExperience { get; private set; }
    public int MaxExperience { get; private set; }
    public decimal MinSalary { get; private set; }
    public decimal MaxSalary { get; private set; }
    public string Currency { get; private set; } = "INR";
    public string Skills { get; private set; } = "[]";
    public string Requirements { get; private set; } = "[]";
    public string Responsibilities { get; private set; } = "[]";
    public string Benefits { get; private set; } = "[]";
    public JobStatus Status { get; private set; }
    public DateTime? PublishedAt { get; private set; }
    public DateTime? ClosedAt { get; private set; }
    public int HeadCount { get; private set; }
    public int FilledCount { get; private set; }
    public Guid HiringManagerId { get; private set; }
    public Guid RecruiterId { get; private set; }
    public bool IsUrgent { get; private set; }
    public DateTime? ApplicationDeadline { get; private set; }
    public Guid TenantId { get; private set; }

    private readonly List<JobApplication> _applications = new();
    public IReadOnlyCollection<JobApplication> Applications => _applications.AsReadOnly();

    private JobPosting() { }

    public static JobPosting Create(
        string title, string description, Guid departmentId, Guid designationId,
        Guid branchId, EmploymentType employmentType, int minExperience, int maxExperience,
        decimal minSalary, decimal maxSalary, string currency, string skills,
        string requirements, string responsibilities, string benefits,
        int headCount, Guid hiringManagerId, Guid recruiterId,
        bool isUrgent, DateTime? applicationDeadline, Guid tenantId)
    {
        return new JobPosting
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            DepartmentId = departmentId,
            DesignationId = designationId,
            BranchId = branchId,
            EmploymentType = employmentType,
            MinExperience = minExperience,
            MaxExperience = maxExperience,
            MinSalary = minSalary,
            MaxSalary = maxSalary,
            Currency = currency,
            Skills = skills,
            Requirements = requirements,
            Responsibilities = responsibilities,
            Benefits = benefits,
            Status = JobStatus.Draft,
            HeadCount = headCount,
            FilledCount = 0,
            HiringManagerId = hiringManagerId,
            RecruiterId = recruiterId,
            IsUrgent = isUrgent,
            ApplicationDeadline = applicationDeadline,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string title, string description, Guid departmentId, Guid designationId,
        Guid branchId, EmploymentType employmentType, int minExperience, int maxExperience,
        decimal minSalary, decimal maxSalary, string currency, string skills,
        string requirements, string responsibilities, string benefits,
        int headCount, Guid hiringManagerId, bool isUrgent, DateTime? applicationDeadline)
    {
        Title = title;
        Description = description;
        DepartmentId = departmentId;
        DesignationId = designationId;
        BranchId = branchId;
        EmploymentType = employmentType;
        MinExperience = minExperience;
        MaxExperience = maxExperience;
        MinSalary = minSalary;
        MaxSalary = maxSalary;
        Currency = currency;
        Skills = skills;
        Requirements = requirements;
        Responsibilities = responsibilities;
        Benefits = benefits;
        HeadCount = headCount;
        HiringManagerId = hiringManagerId;
        IsUrgent = isUrgent;
        ApplicationDeadline = applicationDeadline;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Publish()
    {
        if (Status != JobStatus.Draft && Status != JobStatus.OnHold)
            throw new InvalidOperationException("Only Draft or OnHold jobs can be published.");
        Status = JobStatus.Published;
        PublishedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void PutOnHold()
    {
        if (Status != JobStatus.Published)
            throw new InvalidOperationException("Only Published jobs can be put on hold.");
        Status = JobStatus.OnHold;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Close()
    {
        if (Status == JobStatus.Closed || Status == JobStatus.Filled)
            throw new InvalidOperationException("Job is already closed or filled.");
        Status = JobStatus.Closed;
        ClosedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkFilled()
    {
        Status = JobStatus.Filled;
        ClosedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementFilledCount()
    {
        FilledCount++;
        if (FilledCount >= HeadCount)
            MarkFilled();
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum EmploymentType
{
    FullTime = 0,
    PartTime = 1,
    Contract = 2,
    Intern = 3
}
