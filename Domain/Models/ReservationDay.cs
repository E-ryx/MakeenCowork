namespace Domain.Models;
public class ReservationDay
{
    public int ReservationDayId { get; private set; }
    public int ReservationId { get; private set; }
    public DateOnly Date { get; private set; }

    // Navigation Property
    public Reservation Reservation { get; private set; }

    private ReservationDay() { } // EF Core

    public ReservationDay(int reservationId, DateOnly date)
    {
        ReservationId = reservationId;
        Date = date;
    }
}
