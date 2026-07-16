using HRMS.Services.Attendance.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Domain.Entities;

public class AttendanceRecord : AggregateRoot
{
    public Guid EmployeeId { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime? CheckInTime { get; private set; }
    public DateTime? CheckOutTime { get; private set; }
    public Guid? ShiftId { get; private set; }
    public AttendanceStatus Status { get; private set; }
    public CheckInMethod? CheckInMethod { get; private set; }
    public CheckInMethod? CheckOutMethod { get; private set; }
    public double? CheckInLatitude { get; private set; }
    public double? CheckInLongitude { get; private set; }
    public double? CheckOutLatitude { get; private set; }
    public double? CheckOutLongitude { get; private set; }
    public string? WifiSSID { get; private set; }
    public string? WifiBSSID { get; private set; }
    public string? QrCodeId { get; private set; }
    public decimal? TotalHours { get; private set; }
    public decimal? OvertimeHours { get; private set; }
    public int BreakMinutes { get; private set; }
    public bool IsLate { get; private set; }
    public int LateMinutes { get; private set; }
    public bool IsEarlyExit { get; private set; }
    public int EarlyExitMinutes { get; private set; }
    public string? Notes { get; private set; }
    public Guid? ApprovedBy { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    public bool IsApproved { get; private set; }

    private AttendanceRecord() { }

    public static AttendanceRecord CreateCheckIn(
        Guid employeeId,
        DateTime date,
        Guid? shiftId,
        CheckInMethod method,
        double? latitude = null,
        double? longitude = null,
        string? wifiSsid = null,
        string? wifiBssid = null,
        string? qrCodeId = null,
        Guid? tenantId = null)
    {
        var record = new AttendanceRecord
        {
            EmployeeId = employeeId,
            Date = date.Date,
            CheckInTime = DateTime.UtcNow,
            CheckOutTime = null,
            ShiftId = shiftId,
            Status = AttendanceStatus.Present,
            CheckInMethod = method,
            CheckInLatitude = latitude,
            CheckInLongitude = longitude,
            WifiSSID = wifiSsid,
            WifiBSSID = wifiBssid,
            QrCodeId = qrCodeId,
            BreakMinutes = 0,
            IsApproved = true
        };

        if (tenantId.HasValue)
            record.TenantId = tenantId.Value;

        return record;
    }

    public void CheckOut(CheckInMethod method, double? latitude = null, double? longitude = null)
    {
        if (CheckOutTime.HasValue)
            throw new InvalidOperationException("Employee has already checked out for this record.");

        CheckOutTime = DateTime.UtcNow;
        CheckOutMethod = method;
        CheckOutLatitude = latitude;
        CheckOutLongitude = longitude;
        CalculateTotalHours();
    }

    public void Approve(Guid approvedBy)
    {
        IsApproved = true;
        ApprovedBy = approvedBy;
        ApprovedAt = DateTime.UtcNow;
    }

    public void Reject(Guid approvedBy, string? reason = null)
    {
        IsApproved = false;
        ApprovedBy = approvedBy;
        ApprovedAt = DateTime.UtcNow;
        Notes = reason;
    }

    public void MarkAsLate(int lateMinutes)
    {
        IsLate = true;
        LateMinutes = lateMinutes;
        Status = AttendanceStatus.Late;
    }

    public void MarkAsEarlyExit(int earlyExitMinutes)
    {
        IsEarlyExit = true;
        EarlyExitMinutes = earlyExitMinutes;
    }

    public void SetBreakMinutes(int minutes)
    {
        BreakMinutes = minutes;
    }

    public void UpdateStatus(AttendanceStatus status)
    {
        Status = status;
    }

    public void UpdateNotes(string? notes)
    {
        Notes = notes;
    }

    public void SetShift(Guid? shiftId)
    {
        ShiftId = shiftId;
    }

    public void SetOvertimeHours(decimal? overtimeHours)
    {
        OvertimeHours = overtimeHours;
    }

    public void CalculateTotalHours()
    {
        if (CheckInTime.HasValue && CheckOutTime.HasValue)
        {
            var gross = (CheckOutTime.Value - CheckInTime.Value).TotalMinutes;
            var net = gross - BreakMinutes;
            TotalHours = (decimal)(net / 60.0);
        }
    }

    public int CalculateLateMinutes(DateTime shiftStartTime)
    {
        if (!CheckInTime.HasValue) return 0;
        var late = (CheckInTime.Value - shiftStartTime).TotalMinutes;
        return late > 0 ? (int)late : 0;
    }

    public int CalculateEarlyExit(DateTime shiftEndTime)
    {
        if (!CheckOutTime.HasValue) return 0;
        var early = (shiftEndTime - CheckOutTime.Value).TotalMinutes;
        return early > 0 ? (int)early : 0;
    }

    public decimal CalculateOvertimeHours(decimal overtimeThresholdMinutes)
    {
        if (!TotalHours.HasValue || TotalHours.Value <= 0) return 0;
        var totalMinutes = TotalHours.Value * 60;
        return totalMinutes > overtimeThresholdMinutes
            ? (totalMinutes - overtimeThresholdMinutes) / 60m
            : 0;
    }
}
