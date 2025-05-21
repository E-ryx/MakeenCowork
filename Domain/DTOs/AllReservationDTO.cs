using static Domain.Models.Reservation;

namespace Domain.DTOs;

public class AllReservationDTO
{
    public int ReservationId { get;  set; }
    public int UserId { get;  set; }
    public  string FullName{ get; set; }
    public string NationalCode { get; set; }
    public string PhoneNumber{ get; set; }
    public DateOnly CreateDate{ get; set; }
}