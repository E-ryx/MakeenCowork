namespace Domain.Models;
public class Reservation
{
    public int ReservationId { get; private set; }
    public int UserId { get; private set; }
    public int SpaceId { get; private set; }
    public int TransactionId { get; private set; }
    public int NumberOfPeople { get; private set; }
    public ReservationStatus Status { get; private set; }
    public ReservationState State { get; private set; }
    public bool ExtraServices { get; private set; }
    public DateOnly CreatedAt { get; private set; }

    // Navigation Properties
    public Space Space { get; private set; }
    public List<ReservationDay> Days { get; private set; } = new();

    private Reservation() { } // EF Core

    public Reservation(int userId, int spaceId, int transactionId, int numberOfPeople, ReservationStatus status, bool extraServices, DateOnly createdAt)
    {
        UserId = userId;
        SpaceId = spaceId;
        TransactionId = transactionId;
        NumberOfPeople = numberOfPeople;
        Status = status;
        ExtraServices = extraServices;
        CreatedAt = createdAt;
    }

    public void AddDay(DateOnly date)
    {
        Days.Add(new ReservationDay(ReservationId, date));
    }

    public enum ReservationStatus
    {
        Approved,
        Pending,
        Declined
    }
    public enum ReservationState
    {
        Current,
        Past,
        Cancelled
    }
}
