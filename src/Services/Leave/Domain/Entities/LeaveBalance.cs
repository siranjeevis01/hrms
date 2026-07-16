using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Domain.Entities;

public class LeaveBalance : BaseEntity
{
    private LeaveBalance() { }

    public Guid EmployeeId { get; private set; }
    public Guid LeaveTypeId { get; private set; }
    public int Year { get; private set; }
    public decimal TotalDays { get; private set; }
    public decimal UsedDays { get; private set; }
    public decimal PendingDays { get; private set; }
    public decimal CarryForwardDays { get; private set; }
    public decimal EncashedDays { get; private set; }
    public decimal AdjustedDays { get; private set; }
    public decimal AvailableDays => TotalDays + CarryForwardDays + AdjustedDays - UsedDays - PendingDays - EncashedDays;
    public DateTime? LastAccrualDate { get; private set; }
    public Guid TenantId { get; private set; }

    public static LeaveBalance Create(Guid id, Guid employeeId, Guid leaveTypeId, int year,
        decimal totalDays, Guid tenantId)
    {
        return new LeaveBalance
        {
            Id = id,
            EmployeeId = employeeId,
            LeaveTypeId = leaveTypeId,
            Year = year,
            TotalDays = totalDays,
            UsedDays = 0,
            PendingDays = 0,
            CarryForwardDays = 0,
            EncashedDays = 0,
            AdjustedDays = 0,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Deduct(decimal days)
    {
        var currentAvailable = AvailableDays;
        if (days > currentAvailable)
            throw new InvalidOperationException($"Insufficient leave balance. Available: {currentAvailable}, Requested: {days}");
        PendingDays += days;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Restore(decimal days)
    {
        PendingDays = Math.Max(0, PendingDays - days);
        UsedDays = Math.Min(UsedDays, UsedDays);
        UpdatedAt = DateTime.UtcNow;
    }

    public void Approve(decimal days)
    {
        PendingDays = Math.Max(0, PendingDays - days);
        UsedDays += days;
        UpdatedAt = DateTime.UtcNow;
    }

    public void CarryForward(decimal days)
    {
        CarryForwardDays += days;
        TotalDays = 0;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Encash(decimal days)
    {
        if (days > AvailableDays)
            throw new InvalidOperationException($"Insufficient balance for encashment. Available: {AvailableDays}");
        EncashedDays += days;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Adjust(decimal days, bool isAddition)
    {
        if (isAddition)
            AdjustedDays += days;
        else
        {
            var newAdjusted = AdjustedDays - days;
            if (newAdjusted < 0) throw new InvalidOperationException("Cannot reduce adjusted days below zero.");
            AdjustedDays = newAdjusted;
        }
        UpdatedAt = DateTime.UtcNow;
    }

    public void Accrue(decimal days)
    {
        TotalDays += days;
        LastAccrualDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
