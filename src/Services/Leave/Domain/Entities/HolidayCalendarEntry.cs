using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Domain.Entities;

public class HolidayCalendarEntry : BaseEntity
{
    private HolidayCalendarEntry() { }

    public Guid HolidayId { get; private set; }
    public Guid CompanyId { get; private set; }
    public Guid? BranchId { get; private set; }
    public DateTime Date { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public bool IsOptional { get; private set; }
    public Guid TenantId { get; private set; }

    public static HolidayCalendarEntry Create(
        Guid id,
        Guid holidayId,
        Guid companyId,
        Guid? branchId,
        DateTime date,
        string name,
        bool isOptional,
        Guid tenantId)
    {
        return new HolidayCalendarEntry
        {
            Id = id,
            HolidayId = holidayId,
            CompanyId = companyId,
            BranchId = branchId,
            Date = date,
            Name = name,
            IsOptional = isOptional,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }
}
