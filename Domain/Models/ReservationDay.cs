using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;
public class ReservationDay
{
    [Key]
    public int ReservationDayId { get; private set; }
    public int ReservationId { get; private set; }
    public DateOnly Date { get; private set; }

    public int SpaceId{ get; set; }



    // Navigation Property
    [ForeignKey("ReservationId")]
    public Reservation Reservation { get; private set; }

    private ReservationDay() { } // EF Core

    public ReservationDay(int reservationId, DateOnly date)
    {
        ReservationId = reservationId;
        Date = date;
    }
}
