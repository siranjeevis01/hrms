namespace HRMS.Services.Payroll.Domain.Enums;

public enum PayrollStatus
{
    Draft = 0,
    Processing = 1,
    Processed = 2,
    Approved = 3,
    Paid = 4,
    Locked = 5,
    Reversed = 6
}
