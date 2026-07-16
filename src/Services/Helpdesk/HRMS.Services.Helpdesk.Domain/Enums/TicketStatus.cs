namespace HRMS.Services.Helpdesk.Domain.Enums;

public enum TicketStatus
{
    Open = 0,
    InProgress = 1,
    WaitingOnEmployee = 2,
    WaitingOnThirdParty = 3,
    Resolved = 4,
    Closed = 5
}
