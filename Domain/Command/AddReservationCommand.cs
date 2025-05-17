using MediatR;
using static Domain.Models.Reservation;

namespace Domain.Command;

public class AddReservationCommand : IRequest<string>
{
    public int UserId { get;  set; }
    public int SpaceId { get;  set; }
    public int TransactionId { get;  set; }
    public int NumberOfPeople { get;  set; }
    public bool ExtraServices { get;  set; }
    public DateOnly CreatedAt { get;  set; }
}