using MediatR;

namespace Domain.Command;

public class CancelReserveCommand:IRequest
{
    public int UserId{ get; set; }
    public int ReservationId{ get; set; }
}