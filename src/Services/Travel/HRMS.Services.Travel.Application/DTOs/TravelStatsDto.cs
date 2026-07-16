using HRMS.Services.Travel.Domain.Enums;

namespace HRMS.Services.Travel.Application.DTOs;

public class TravelStatsDto
{
    public int TotalRequests { get; set; }
    public int DraftRequests { get; set; }
    public int SubmittedRequests { get; set; }
    public int ApprovedRequests { get; set; }
    public int RejectedRequests { get; set; }
    public int InProgressRequests { get; set; }
    public int CompletedRequests { get; set; }
    public int CancelledRequests { get; set; }
    public decimal TotalEstimatedCost { get; set; }
    public decimal TotalActualCost { get; set; }
    public int PendingVisaRequests { get; set; }
    public Dictionary<TransportMode, int> RequestsByTransport { get; set; } = new();
}
