using HRMS.Services.Travel.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Travel.Domain.Entities;

public class TravelRequest : AggregateRoot
{
    public Guid EmployeeId { get; private set; }
    public string Purpose { get; private set; } = string.Empty;
    public string Destination { get; private set; } = string.Empty;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public TravelRequestStatus Status { get; private set; }
    public decimal EstimatedCost { get; private set; }
    public decimal? ActualCost { get; private set; }
    public string Currency { get; private set; } = "USD";
    public TransportMode TransportMode { get; private set; }
    public AccommodationType AccommodationType { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private readonly List<TravelItinerary> _itineraries = new();
    public IReadOnlyCollection<TravelItinerary> Itineraries => _itineraries.AsReadOnly();

    private readonly List<TravelExpense> _expenses = new();
    public IReadOnlyCollection<TravelExpense> Expenses => _expenses.AsReadOnly();

    private readonly List<TravelApproval> _approvals = new();
    public IReadOnlyCollection<TravelApproval> Approvals => _approvals.AsReadOnly();

    private TravelRequest() { }

    public static TravelRequest Create(
        Guid employeeId,
        string purpose,
        string destination,
        DateTime startDate,
        DateTime endDate,
        decimal estimatedCost,
        string currency,
        TransportMode transportMode,
        AccommodationType accommodationType,
        string tenantId)
    {
        return new TravelRequest
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            Purpose = purpose,
            Destination = destination,
            StartDate = startDate,
            EndDate = endDate,
            Status = TravelRequestStatus.Draft,
            EstimatedCost = estimatedCost,
            Currency = currency,
            TransportMode = transportMode,
            AccommodationType = accommodationType,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Submit()
    {
        if (Status != TravelRequestStatus.Draft)
            throw new InvalidOperationException("Only draft requests can be submitted.");

        Status = TravelRequestStatus.Submitted;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Approve()
    {
        if (Status != TravelRequestStatus.Submitted)
            throw new InvalidOperationException("Only submitted requests can be approved.");

        Status = TravelRequestStatus.Approved;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reject()
    {
        if (Status != TravelRequestStatus.Submitted)
            throw new InvalidOperationException("Only submitted requests can be rejected.");

        Status = TravelRequestStatus.Rejected;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        if (Status != TravelRequestStatus.InProgress)
            throw new InvalidOperationException("Only in-progress requests can be completed.");

        Status = TravelRequestStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status == TravelRequestStatus.Completed || Status == TravelRequestStatus.Cancelled)
            throw new InvalidOperationException("Completed or cancelled requests cannot be cancelled.");

        Status = TravelRequestStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(
        string? purpose,
        string? destination,
        DateTime? startDate,
        DateTime? endDate,
        decimal? estimatedCost,
        string? currency,
        TransportMode? transportMode,
        AccommodationType? accommodationType)
    {
        if (Status != TravelRequestStatus.Draft)
            throw new InvalidOperationException("Only draft requests can be updated.");

        Purpose = purpose ?? Purpose;
        Destination = destination ?? Destination;
        StartDate = startDate ?? StartDate;
        EndDate = endDate ?? EndDate;
        EstimatedCost = estimatedCost ?? EstimatedCost;
        Currency = currency ?? Currency;
        TransportMode = transportMode ?? TransportMode;
        AccommodationType = accommodationType ?? AccommodationType;
        UpdatedAt = DateTime.UtcNow;
    }
}
